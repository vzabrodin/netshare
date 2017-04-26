using System;
using System.Collections.Generic;
using System.Linq;
using NetShare.ICS;
using NetShare.WLAN;
using System.ServiceModel;

namespace NetShare.Host
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class NetShareHost
	{
		private WlanManager _wlanManager;
		private IcsManager _icsManager;

		private SharableConnection _currentSharedConnection;

		private string _lastErrorMessage;

		public delegate void GenericEvent(NetShareHost hostedNetworkManager);

		public event GenericEvent ConnectionsListChanged;
		public event GenericEvent HostedNetworkStarted;
		public event GenericEvent HostedNetworkStopped;
		public event GenericEvent HostedNetworkAvailable;

		public NetShareHost()
		{
			_wlanManager = new WlanManager();
			_icsManager = new IcsManager();

			_wlanManager.StationJoin += _wlanManager_StationStateChange;
			_wlanManager.StationLeave += _wlanManager_StationStateChange;
			//_wlanManager.StationStateChange += _wlanManager_StationStateChange;
			_wlanManager.HostedNetworkStarted += _wlanManager_HostedNetworkStarted;
			_wlanManager.HostedNetworkStopped += _wlanManager_HostedNetworkStopped;
			_wlanManager.HostedNetworkAvailable += _wlanManager_HostedNetworkAvailable;
		}

		private void _wlanManager_HostedNetworkAvailable(object sender, EventArgs e)
		{
			if (HostedNetworkAvailable != null)
			{
				HostedNetworkAvailable(this);
			}
		}

		#region "Event Handlers"

		private void _wlanManager_HostedNetworkStopped(object sender, EventArgs e)
		{
			if (HostedNetworkStopped != null)
			{
				HostedNetworkStopped(this);
			}
		}

		private void _wlanManager_HostedNetworkStarted(object sender, EventArgs e)
		{
			if (HostedNetworkStarted != null)
			{
				HostedNetworkStarted(this);
			}
		}

		private void _wlanManager_StationStateChange(object sender, EventArgs e)
		{
			if (ConnectionsListChanged != null)
			{
				ConnectionsListChanged(this);
			}
		}

		#endregion

		public string GetLastError()
		{
			return _lastErrorMessage;
		}

		public bool Start(Guid sharedConnectionGuid)
		{
			var conns = GetSharableConnections();
			SharableConnection sharedConnection = null;
			sharedConnection = (from c in conns
								where c.Guid == sharedConnectionGuid
								select c).FirstOrDefault();
			if (sharedConnection == null)
			{
				return false;
			}
			return Start(sharedConnection);
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
			List<IcsConnection> connections;
			try
			{
				connections = _icsManager.Connections;
			}
			catch
			{
				connections = new List<IcsConnection>();
			}

			// Empty item to signify No Connection Sharing
			yield return new SharableConnection() { DeviceName = "None", Guid = Guid.Empty, Name = "None" };

			if (connections != null)
			{
				foreach (var conn in connections)
				{
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

		public bool IsStarted
		{
			get
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
		}

		public bool IsSupported
		{
			get
			{
				return _wlanManager.IsSupported;
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