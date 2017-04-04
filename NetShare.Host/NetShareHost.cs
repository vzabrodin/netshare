﻿using System;
using System.Collections.Generic;
using System.Linq;
using NetShare.ICS;
using NetShare.Wlan;
using System.ServiceModel;

namespace NetShare.Host
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class NetShareHost// : INetShareHost
	{
		private WlanManager _wlanManager;
		private IcsManager _icsManager;

		private SharableConnection _currentSharedConnection;

		private string _lastErrorMessage;

		public delegate void GenericEvent(NetShareHost hostedNetworkManager);

		public event GenericEvent OnConnectionsListChanged;

		public NetShareHost()
		{
			_wlanManager = new WlanManager();
			_icsManager = new IcsManager();

			_wlanManager.StationJoin += _wlanManager_StationStateChange;
			_wlanManager.StationLeave += _wlanManager_StationStateChange;
			_wlanManager.StationStateChange += _wlanManager_StationStateChange;
		}

		private void _wlanManager_StationStateChange(object sender, EventArgs e)
		{
			if (OnConnectionsListChanged != null)
			{
				OnConnectionsListChanged(this);
			}
		}

		public string GetLastError()
		{
			return _lastErrorMessage;
		}

		public bool Start(SharableConnection sharedConnection)
		{
			try
			{
				Stop();

				_wlanManager.StartHostedNetwork();

				System.Threading.Thread.Sleep(1000);

				if (sharedConnection != null)
				{
					if (sharedConnection.Guid != Guid.Empty)
					{
						if (_icsManager.SharingInstalled)
						{
							_icsManager.DisableIcsOnAll();

							var privateConnectionGuid = _wlanManager.HostedNetworkInterfaceGuid;

							if (privateConnectionGuid == Guid.Empty)
							{
								// If the GUID for the Hosted Network Adapter isn't return properly,
								// then retrieve it by the DeviceName.

								privateConnectionGuid = (from c in _icsManager.Connections
														 where c.Props.DeviceName.ToLowerInvariant().Contains("microsoft virtual wifi miniport adapter") // Windows 7
														 || c.Props.DeviceName.ToLowerInvariant().Contains("microsoft hosted network virtual adapter") // Windows 8
														 select c.Guid).FirstOrDefault();
								// Note: For some reason the DeviceName can have different names, currently it checks for the ones that I have identified thus far.

								if (privateConnectionGuid == Guid.Empty)
								{
									// Device still now found, so throw exception so the message gets raised up to the client.
									throw new Exception("Virtual Wifi device not found!\n\nNeither \"Microsoft Hosted Network Virtual Adapter\" or \"Microsoft Virtual Wifi Miniport Adapter\" were found.");
								}
							}

							_icsManager.EnableIcs(sharedConnection.Guid, privateConnectionGuid);

							_currentSharedConnection = sharedConnection;
						}
					}
				}
				else
				{
					_currentSharedConnection = null;
				}

				return true;
			}
			catch (Exception ex)
			{
				_lastErrorMessage = ex.Message;
				return false;
			}
		}

		public bool Stop()
		{
			try
			{
				if (_icsManager.SharingInstalled)
				{
					_icsManager.DisableIcsOnAll();
				}

				_wlanManager.StopHostedNetwork();

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool SetConnectionSettings(string ssid, int maxNumberOfPeers)
		{
			try
			{
				_wlanManager.SetConnectionSettings(ssid, maxNumberOfPeers);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public ConnectionSettings GetConnectionSettings()
		{
			try
			{
				string ssid;
				int maxNumberOfPeers;

				var r = _wlanManager.QueryConnectionSettings(out ssid, out maxNumberOfPeers);

				return new ConnectionSettings()
				{
					SSID = ssid,
					MaxPeerCount = maxNumberOfPeers
				};
			}
			catch
			{
				return null;
			}
		}

		public IEnumerable<SharableConnection> GetSharableConnections()
		{
			List<Ics> connections;
			try
			{
				connections = _icsManager.Connections;
			}
			catch
			{
				connections = new List<Ics>();
			}

			// Empty item to signify No Connection Sharing
			yield return new SharableConnection() { DeviceName = "None", Guid = Guid.Empty, Name = "None" };

			if (connections != null)
			{
				foreach (var conn in connections)
				{
					//if (conn.IsConnected && conn.IsSupported)
					if (conn.IsSupported)
					{
						yield return new SharableConnection(conn);
					}
				}
			}
		}

		public bool SetPassword(string password)
		{
			try
			{
				_wlanManager.SetSecondaryKey(password);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public string GetPassword()
		{
			try
			{
				string passKey = string.Empty;
				bool isPassPhrase;
				bool isPersistent;

				var r = _wlanManager.QuerySecondaryKey(out passKey, out isPassPhrase, out isPersistent);

				return passKey;
			}
			catch
			{
				return null;
			}
		}

		public bool IsStarted()
		{
			try
			{
				return _wlanManager.IsHostedNetworkStarted;
			}
			catch
			{
				return false;
			}
		}

		public IEnumerable<ConnectedPeer> GetConnectedPeers()
		{
			foreach (var v in _wlanManager.Stations)
			{
				yield return new ConnectedPeer(v.Value);
			}
		}

		public SharableConnection GetSharedConnection()
		{
			return _currentSharedConnection;
		}
	}
}