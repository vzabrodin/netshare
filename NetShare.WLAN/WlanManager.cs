using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NetShare.WLAN.WinAPI;

namespace NetShare.WLAN
{
	public class WlanManager : IDisposable
	{
		private IntPtr _WlanHandle;
		private uint _ServerVersion;
		private wlanapi.WLAN_NOTIFICATION_CALLBACK _notificationCallback;

		/// <summary>
		/// Creates an instance of WlanManager class
		/// </summary>
		public WlanManager()
		{
			_notificationCallback = new wlanapi.WLAN_NOTIFICATION_CALLBACK(OnNotification);
			Init();
		}

		/// <summary>
		/// Initializes access point
		/// </summary>
		private void Init()
		{
			try
			{
				WlanUtils.ThrowOnWin32Error(
					wlanapi.WlanOpenHandle(
						wlanapi.WLAN_CLIENT_VERSION_VISTA,
						IntPtr.Zero,
						out _ServerVersion,
						ref _WlanHandle));

				WLAN_NOTIFICATION_SOURCE notificationSource;
				WlanUtils.ThrowOnWin32Error(
					wlanapi.WlanRegisterNotification(
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
				wlanapi.WlanCloseHandle(_WlanHandle, IntPtr.Zero);
				throw;
			}
		}

		#region "Events"
		/// <summary>
		/// Raises when access point is started
		/// </summary>
		public event EventHandler HostedNetworkStarted;

		/// <summary>
		/// Raises when access point is stopped
		/// </summary>
		public event EventHandler HostedNetworkStopped;

		/// <summary>
		/// Raises when access point is available
		/// </summary>
		public event EventHandler HostedNetworkAvailable;

		/// <summary>
		/// Raises when new user joined to access point
		/// </summary>
		public event EventHandler StationJoin;

		/// <summary>
		/// Raises when new user leaved access point
		/// </summary>
		public event EventHandler StationLeave;

		/// <summary>
		/// Raises when user's state changes
		/// </summary>
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

		/// <summary>
		/// Force starts hosted network
		/// </summary>
		/// <returns>fail reason</returns>
		public WLAN_HOSTED_NETWORK_REASON ForceStart()
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			WlanUtils.ThrowOnWin32Error(
				wlanapi.WlanHostedNetworkForceStart(
					_WlanHandle,
					out failReason,
					IntPtr.Zero));

			return failReason;
		}

		/// <summary>
		/// Force stops hosted network
		/// </summary>
		/// <returns>fail reason</returns>
		public WLAN_HOSTED_NETWORK_REASON ForceStop()
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			WlanUtils.ThrowOnWin32Error(
				wlanapi.WlanHostedNetworkForceStop(
					_WlanHandle,
					out failReason,
					IntPtr.Zero));

			return failReason;
		}

		/// <summary>
		/// Starts hosted network
		/// </summary>
		/// <returns>fail reason</returns>
		public WLAN_HOSTED_NETWORK_REASON StartUsing()
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			WlanUtils.ThrowOnWin32Error(
				wlanapi.WlanHostedNetworkStartUsing(
					_WlanHandle,
					out failReason,
					IntPtr.Zero));

			return failReason;
		}

		/// <summary>
		/// Stops hosted network
		/// </summary>
		/// <returns>fail reason</returns>
		public WLAN_HOSTED_NETWORK_REASON StopUsing()
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			WlanUtils.ThrowOnWin32Error(
				wlanapi.WlanHostedNetworkStopUsing(
					_WlanHandle,
					out failReason,
					IntPtr.Zero));

			return failReason;
		}

		/// <summary>
		/// Initializes hosted network settings
		/// </summary>
		/// <returns>fail reason</returns>
		public WLAN_HOSTED_NETWORK_REASON InitSettings()
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			WlanUtils.ThrowOnWin32Error(
				wlanapi.WlanHostedNetworkInitSettings(
					_WlanHandle,
					out failReason,
					IntPtr.Zero));
			return failReason;
		}


		/// <summary>
		/// Gets AP's password
		/// </summary>
		/// <param name="passKey">password of hosted network</param>
		/// <param name="isPassPhrase">indicates if the key data is in passphrase format</param>
		/// <param name="isPersistent">indicates if the key data is to be stored and reused later or is for one-time use only</param>
		/// <returns>fail reason</returns>
		public WLAN_HOSTED_NETWORK_REASON QuerySecondaryKey(out string passKey, out bool isPassPhrase, out bool isPersistent)
		{
			WLAN_HOSTED_NETWORK_REASON failReason;
			uint keyLen;
			WlanUtils.ThrowOnWin32Error(
				wlanapi.WlanHostedNetworkQuerySecondaryKey(
					_WlanHandle,
					out keyLen,
					out passKey,
					out isPassPhrase,
					out isPersistent,
					out failReason,
					IntPtr.Zero));
			return failReason;
		}

		/// <summary>
		/// Sets a password of hosted network
		/// </summary>
		/// <param name="passKey">password</param>
		/// <returns>fail reason</returns>
		public WLAN_HOSTED_NETWORK_REASON SetSecondaryKey(string passKey)
		{
			WLAN_HOSTED_NETWORK_REASON failReason;

			WlanUtils.ThrowOnWin32Error(
				wlanapi.WlanHostedNetworkSetSecondaryKey(
					_WlanHandle,
					(uint)(passKey.Length + 1),
					passKey,
					true,
					true,
					out failReason,
					IntPtr.Zero));
			return failReason;
		}

		/// <summary>
		/// Gets a status of hosted network
		/// </summary>
		/// <returns>hosted network status</returns>
		public WLAN_HOSTED_NETWORK_STATUS QueryStatus()
		{
			IntPtr ptr = new IntPtr();

			WlanUtils.ThrowOnWin32Error(
				wlanapi.WlanHostedNetworkQueryStatus(
					_WlanHandle,
					out ptr,
					IntPtr.Zero));

			var status = (WLAN_HOSTED_NETWORK_STATUS)Marshal.PtrToStructure(ptr, typeof(WLAN_HOSTED_NETWORK_STATUS));

			return status;
		}

		/// <summary>
		/// Sets hosted network parameters
		/// </summary>
		/// <param name="hostedNetworkSSID">SSID</param>
		/// <param name="maxNumberOfPeers">max number of peers</param>
		/// <returns>fail reason</returns>
		public WLAN_HOSTED_NETWORK_REASON SetConnectionSettings(string hostedNetworkSSID, int maxNumberOfPeers)
		{
			WLAN_HOSTED_NETWORK_REASON failReason;

			WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS settings = new WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS();
			settings.hostedNetworkSSID = hostedNetworkSSID.ToDOT11_SSID();
			settings.dwMaxNumberOfPeers = maxNumberOfPeers;

			IntPtr settingsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(settings));
			Marshal.StructureToPtr(settings, settingsPtr, true);

			WlanUtils.ThrowOnWin32Error(
				wlanapi.WlanHostedNetworkSetProperty(
					_WlanHandle,
					WLAN_HOSTED_NETWORK_OPCODE.CONNECTION_SETTINGS,
					(uint)Marshal.SizeOf(settings),
					settingsPtr,
					out failReason,
					IntPtr.Zero));
			return failReason;
		}

		/// <summary>
		/// Gets hosted network parameters
		/// </summary>
		/// <param name="hostedNetworkSSID">SSID</param>
		/// <param name="maxNumberOfPeers">max number of peers</param>
		/// <returns>тип значения</returns>
		public WLAN_OPCODE_VALUE_TYPE QueryConnectionSettings(out string hostedNetworkSSID, out int maxNumberOfPeers)
		{
			uint dataSize;
			IntPtr dataPtr;
			WLAN_OPCODE_VALUE_TYPE opcode;

			WlanUtils.ThrowOnWin32Error(
				wlanapi.WlanHostedNetworkQueryProperty(
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

			hostedNetworkSSID = settings.hostedNetworkSSID.ucSSID;

			maxNumberOfPeers = (int)settings.dwMaxNumberOfPeers;

			return opcode;
		}

		/// <summary>
		/// Starts hosted network
		/// </summary>
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

		/// <summary>
		/// Stops hosted network
		/// </summary>
		public void StopHostedNetwork()
		{
			ForceStop();
		}

		#endregion

		#region "Properties"

		/// <summary>
		/// GUID виртуального адаптера точки доступа
		/// </summary>
		public Guid HostedNetworkInterfaceGuid
		{
			get
			{
				var status = QueryStatus();
				return status.IPDeviceID;
			}
		}

		/// <summary>
		/// Состояние точки доступа
		/// </summary>
		public WLAN_HOSTED_NETWORK_STATE HostedNetworkState
		{
			get
			{
				return QueryStatus().HostedNetworkState;
			}
		}

		/// <summary>
		/// Подключенные пользователи
		/// </summary>
		public Dictionary<string, WlanStation> Stations
		{
			get
			{
				Dictionary<string, WlanStation> stations = new Dictionary<string, WlanStation>();

				IntPtr ptr = new IntPtr();

				WlanUtils.ThrowOnWin32Error(
					wlanapi.WlanHostedNetworkQueryStatus(
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
						stations.Add(peer.PeerMacAddress.ConvertToString(), new WlanStation(peer));

						offset += Marshal.SizeOf(peer);
					}
				}
				return stations;
			}
		}

		/// <summary>
		/// Показывает, запущена ли точка доступа
		/// </summary>
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
				wlanapi.WlanCloseHandle(_WlanHandle, IntPtr.Zero);
			}
		}

		#endregion
	}
}