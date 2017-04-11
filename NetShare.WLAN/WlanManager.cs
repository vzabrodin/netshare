using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NetShare.Wlan.WinAPI;

namespace NetShare.Wlan
{
	public class WlanManager : IDisposable
	{
		private IntPtr _WlanHandle;
		private uint _ServerVersion;
		private WlanApi.WLAN_NOTIFICATION_CALLBACK _notificationCallback;

		public WlanManager()
		{
			_notificationCallback = new WlanApi.WLAN_NOTIFICATION_CALLBACK(OnNotification);
			Init();
		}

		private void Init()
		{
			try
			{
				WlanUtils.ThrowOnWin32Error(
					WlanApi.WlanOpenHandle(
						WlanApi.WLAN_CLIENT_VERSION_VISTA,
						IntPtr.Zero,
						out _ServerVersion,
						ref _WlanHandle));

				WLAN_NOTIFICATION_SOURCE notificationSource;
				WlanUtils.ThrowOnWin32Error(
					WlanApi.WlanRegisterNotification(
						_WlanHandle,
						WLAN_NOTIFICATION_SOURCE.ALL,
						true,
						_notificationCallback,
						IntPtr.Zero,
						IntPtr.Zero,
						out notificationSource));

				WLAN_HOSTED_NETWORK_REASON failReason = InitSettings();
				if (failReason != WLAN_HOSTED_NETWORK_REASON.SUCCESS)
				{
					throw new Exception("Init Error WlanHostedNetworkInitSettings: " + failReason.ToString());
				}
			}
			catch
			{
				WlanApi.WlanCloseHandle(_WlanHandle, IntPtr.Zero);
				throw;
			}
		}

		#region "Events"

		public event EventHandler HostedNetworkStarted;
		public event EventHandler HostedNetworkStopped;
		public event EventHandler HostedNetworkAvailable;

		public event EventHandler StationJoin;
		public event EventHandler StationLeave;
		public event EventHandler StationStateChange;

		#endregion

		#region "OnNotification"

		protected void onHostedNetworkStarted()
		{
			if (HostedNetworkStarted != null)
			{
				HostedNetworkStarted(this, EventArgs.Empty);
			}
		}

		protected void onHostedNetworkStopped()
		{
			if (HostedNetworkStopped != null)
			{
				HostedNetworkStopped(this, EventArgs.Empty);
			}
		}

		protected void onHostedNetworkAvailable()
		{
			if (HostedNetworkAvailable != null)
			{
				HostedNetworkAvailable(this, EventArgs.Empty);
			}
		}

		protected void onStationJoin(WLAN_HOSTED_NETWORK_PEER_STATE stationState)
		{

			var pStation = new WlanStation(stationState);

			if (StationJoin != null)
			{
				StationJoin(this, EventArgs.Empty);
			}
		}

		protected void onStationLeave(WLAN_HOSTED_NETWORK_PEER_STATE stationState)
		{
			if (StationLeave != null)
			{
				StationLeave(this, EventArgs.Empty);
			}
		}

		protected void onStationStateChange(WLAN_HOSTED_NETWORK_PEER_STATE stationState)
		{
			if (StationStateChange != null)
			{
				StationStateChange(this, EventArgs.Empty);
			}
		}

		protected void OnNotification(ref WLAN_NOTIFICATION_DATA notificationData, IntPtr context)
		{
			switch ((int)notificationData.NotificationCode)
			{
				case (int)WLAN_HOSTED_NETWORK_NOTIFICATION_CODE.STATE_CHANGE:

					if (notificationData.dwDataSize > 0 && notificationData.pData != IntPtr.Zero)
					{
						WLAN_HOSTED_NETWORK_STATE_CHANGE pStateChange =
							(WLAN_HOSTED_NETWORK_STATE_CHANGE)Marshal.PtrToStructure(
								notificationData.pData,
								typeof(WLAN_HOSTED_NETWORK_STATE_CHANGE));

						switch (pStateChange.NewState)
						{
							case WLAN_HOSTED_NETWORK_STATE.ACTIVE:
								onHostedNetworkStarted();
								break;

							case WLAN_HOSTED_NETWORK_STATE.IDLE:
								if (pStateChange.OldState == WLAN_HOSTED_NETWORK_STATE.ACTIVE)
								{
									onHostedNetworkStopped();
								}
								else
								{
									onHostedNetworkAvailable();
								}
								break;

							case WLAN_HOSTED_NETWORK_STATE.UNAVAILABLE:
								if (pStateChange.OldState == WLAN_HOSTED_NETWORK_STATE.ACTIVE)
								{
									onHostedNetworkStopped();
								}
								onHostedNetworkAvailable();
								break;
						}
					}

					break;

				case (int)WLAN_HOSTED_NETWORK_NOTIFICATION_CODE.PEER_STATE_CHANGE:

					if (notificationData.dwDataSize > 0 && notificationData.pData != IntPtr.Zero)
					{
						WLAN_HOSTED_NETWORK_DATA_PEER_STATE_CHANGE pPeerStateChange = (WLAN_HOSTED_NETWORK_DATA_PEER_STATE_CHANGE)Marshal.PtrToStructure(notificationData.pData, typeof(WLAN_HOSTED_NETWORK_DATA_PEER_STATE_CHANGE));

						if (pPeerStateChange.NewState.PeerAuthState == WLAN_HOSTED_NETWORK_PEER_AUTH_STATE.AUTHENTICATED)
						{
							// Station joined the hosted network
							onStationJoin(pPeerStateChange.NewState);
						}
						else if (pPeerStateChange.NewState.PeerAuthState == WLAN_HOSTED_NETWORK_PEER_AUTH_STATE.INVALID)
						{
							// Station left the hosted network
							onStationLeave(pPeerStateChange.NewState);
						}
						else
						{
							// Authentication state changed
							onStationStateChange(pPeerStateChange.NewState);
						}
					}

					break;

				case (int)WLAN_HOSTED_NETWORK_NOTIFICATION_CODE.RADIO_STATE_CHANGE:
					if (notificationData.dwDataSize > 0 && notificationData.pData != IntPtr.Zero)
					{
						//WLAN_HOSTED_NETWORK_RADIO_STATE pRadioState = (WLAN_HOSTED_NETWORK_RADIO_STATE)Marshal.PtrToStructure(notifData.dataPtr, typeof(WLAN_HOSTED_NETWORK_RADIO_STATE));
						// Do nothing for now
					}
					//else
					//{
					//    // // Shall NOT happen
					//    // _ASSERT(FAILSE);
					//}
					break;
			}
		}

		#endregion

		#region "Public Methods"

		public WLAN_HOSTED_NETWORK_REASON ForceStart()
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			WlanUtils.ThrowOnWin32Error(
				WlanApi.WlanHostedNetworkForceStart(
					_WlanHandle,
					out failReason,
					IntPtr.Zero));

			return failReason;
		}

		public WLAN_HOSTED_NETWORK_REASON ForceStop()
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			WlanUtils.ThrowOnWin32Error(
				WlanApi.WlanHostedNetworkForceStop(
					_WlanHandle,
					out failReason,
					IntPtr.Zero));

			return failReason;
		}

		public WLAN_HOSTED_NETWORK_REASON StartUsing()
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			WlanUtils.ThrowOnWin32Error(
				WlanApi.WlanHostedNetworkStartUsing(
					_WlanHandle,
					out failReason,
					IntPtr.Zero));

			return failReason;
		}

		public WLAN_HOSTED_NETWORK_REASON StopUsing()
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			WlanUtils.ThrowOnWin32Error(
				WlanApi.WlanHostedNetworkStopUsing(
					_WlanHandle,
					out failReason,
					IntPtr.Zero));

			return failReason;
		}

		public WLAN_HOSTED_NETWORK_REASON InitSettings()
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			WlanUtils.ThrowOnWin32Error(
				WlanApi.WlanHostedNetworkInitSettings(
					_WlanHandle,
					out failReason,
					IntPtr.Zero));
			return failReason;
		}

		public WLAN_HOSTED_NETWORK_REASON QuerySecondaryKey(out string passKey, out bool isPassPhrase, out bool isPersistent)
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			uint keyLen;
			WlanUtils.ThrowOnWin32Error(
				WlanApi.WlanHostedNetworkQuerySecondaryKey(
					_WlanHandle,
					out keyLen,
					out passKey,
					out isPassPhrase,
					out isPersistent,
					out failReason,
					IntPtr.Zero));
			return failReason;
		}

		public WLAN_HOSTED_NETWORK_REASON SetSecondaryKey(string passKey)
		{
			WLAN_HOSTED_NETWORK_REASON failReason;

			WlanUtils.ThrowOnWin32Error(
				WlanApi.WlanHostedNetworkSetSecondaryKey(
					_WlanHandle,
					(uint)(passKey.Length + 1),
					passKey,
					true,
					true,
					out failReason,
					IntPtr.Zero));
			return failReason;
		}

		public WLAN_HOSTED_NETWORK_STATUS QueryStatus()
		{
			IntPtr ptr = new IntPtr();

			WlanUtils.ThrowOnWin32Error(
				WlanApi.WlanHostedNetworkQueryStatus(
					_WlanHandle,
					out ptr,
					IntPtr.Zero));

			var status = (WLAN_HOSTED_NETWORK_STATUS)Marshal.PtrToStructure(ptr, typeof(WLAN_HOSTED_NETWORK_STATUS));

			return status;
		}

		public WLAN_HOSTED_NETWORK_REASON SetConnectionSettings(string hostedNetworkSSID, int maxNumberOfPeers)
		{
			WLAN_HOSTED_NETWORK_REASON failReason;

			WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS settings = new WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS();
			settings.hostedNetworkSSID = WlanUtils.ConvertStringToDOT11_SSID(hostedNetworkSSID);
			settings.dwMaxNumberOfPeers = (uint)maxNumberOfPeers;

			IntPtr settingsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(settings));
			Marshal.StructureToPtr(settings, settingsPtr, false);

			WlanUtils.ThrowOnWin32Error(
				WlanApi.WlanHostedNetworkSetProperty(
					_WlanHandle,
					WLAN_HOSTED_NETWORK_OPCODE.CONNECTION_SETTINGS,
					(uint)Marshal.SizeOf(settings),
					settingsPtr,
					out failReason,
					IntPtr.Zero));
			return failReason;
		}

		public WLAN_OPCODE_VALUE_TYPE QueryConnectionSettings(out string hostedNetworkSSID, out int maxNumberOfPeers)
		{
			uint dataSize;
			IntPtr dataPtr;
			WLAN_OPCODE_VALUE_TYPE opcode;

			WlanUtils.ThrowOnWin32Error(
				WlanApi.WlanHostedNetworkQueryProperty(
					_WlanHandle,
					WLAN_HOSTED_NETWORK_OPCODE.CONNECTION_SETTINGS,
					out dataSize,
					out dataPtr,
					out opcode,
					IntPtr.Zero));

			var settings = 
				(WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS)Marshal.PtrToStructure(
					dataPtr,
					typeof(WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS));

			hostedNetworkSSID = WlanUtils.ToString(settings.hostedNetworkSSID);

			maxNumberOfPeers = (int)settings.dwMaxNumberOfPeers;

			return opcode;
		}

		public void StartHostedNetwork()
		{
			try
			{
				ForceStop();

				var failReason = StartUsing();
				if (failReason != WLAN_HOSTED_NETWORK_REASON.SUCCESS)
				{
					throw new Exception("Could not start hosted network\n\n" + failReason.ToString());
				}
			}
			catch
			{
				ForceStop();
				throw;
			}
		}

		public void StopHostedNetwork()
		{
			ForceStop();
		}

		#endregion

		#region "Properties"

		public Guid HostedNetworkInterfaceGuid
		{
			get
			{
				var status = QueryStatus();
				return status.IPDeviceID;
			}
		}

		public WLAN_HOSTED_NETWORK_STATE HostedNetworkState
		{
			get
			{
				return QueryStatus().HostedNetworkState;
			}
		}

		public Dictionary<string, WlanStation> Stations
		{
			get
			{
				Dictionary<string, WlanStation> stations = new Dictionary<string, WlanStation>();

				IntPtr ptr = new IntPtr();

				WlanUtils.ThrowOnWin32Error(
					WlanApi.WlanHostedNetworkQueryStatus(
						_WlanHandle,
						out ptr,
						IntPtr.Zero));

				var status = (WLAN_HOSTED_NETWORK_STATUS)Marshal.PtrToStructure(ptr, typeof(WLAN_HOSTED_NETWORK_STATUS));

				if (status.HostedNetworkState != WLAN_HOSTED_NETWORK_STATE.UNAVAILABLE)
				{
					IntPtr offset = Marshal.OffsetOf(typeof(WLAN_HOSTED_NETWORK_STATUS), "PeerList");

					for (int i = 0; i < status.dwNumberOfPeers; i++)
					{
						var peer = (WLAN_HOSTED_NETWORK_PEER_STATE)Marshal.PtrToStructure(
							new IntPtr(ptr.ToInt64() + offset.ToInt64()),
							typeof(WLAN_HOSTED_NETWORK_PEER_STATE));
						stations.Add(WlanUtils.ToString(peer.PeerMacAddress), new WlanStation(peer));

						offset += Marshal.SizeOf(peer);
					}
				}
				return stations;
			}
		}

		public bool IsHostedNetworkStarted
		{
			get
			{
				return (HostedNetworkState == WLAN_HOSTED_NETWORK_STATE.ACTIVE);
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			ForceStop();

			if (_WlanHandle != IntPtr.Zero)
			{
				WlanApi.WlanCloseHandle(_WlanHandle, IntPtr.Zero);
			}
		}

		#endregion
	}
}