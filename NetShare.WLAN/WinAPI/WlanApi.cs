using System;
using System.Runtime.InteropServices;

namespace NetShare.Wlan.WinAPI
{
	public static class WlanApi
	{
		/// <summary>
		/// Client version of Windows XP with SP3 and Wireless LAN API for Windows XP with SP2
		/// </summary>
		public const uint WLAN_CLIENT_VERSION_XP = 1;

		/// <summary>
		/// Client version for Windows Vista and Windows Server 2008
		/// </summary>
		public const uint WLAN_CLIENT_VERSION_VISTA = 2;

		public const uint WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_ADHOC_PROFILES = 0x00000001;
		public const uint WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_MANUAL_HIDDEN_PROFILES = 0x00000002;

		public const uint WLAN_CONNECTION_HIDDEN_NETWORK = 0x00000001;
		public const uint WLAN_CONNECTION_ADHOC_JOIN_ONLY = 0x00000002;
		public const uint WLAN_CONNECTION_IGNORE_PRIVACY_BIT = 0x00000004;
		public const uint WLAN_CONNECTION_EAPOL_PASSTHROUGH = 0x00000008;


		[DllImport("wlanapi.dll", SetLastError = true)]
		public static extern uint WlanConnect(
			IntPtr hClientHandle,
			ref Guid pInterfaceGuid,
			ref WLAN_CONNECTION_PARAMETERS pConnectionParameters,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", EntryPoint = "WlanCloseHandle")]
		public static extern uint WlanCloseHandle(
			[In] IntPtr hClientHandle,
			IntPtr pReserved);


		[DllImport("wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern uint WlanDeleteProfile(IntPtr hClientHandle,
			ref Guid pInterfaceGuid,
			string strProfileName,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", SetLastError = true)]
		public static extern uint WlanDisconnect(
			IntPtr hClientHandle,
			ref Guid pInterfaceGuid,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", EntryPoint = "WlanEnumInterfaces")]
		public static extern uint WlanEnumInterfaces(
			[In] IntPtr hClientHandle,
			IntPtr pReserved,
			ref IntPtr ppInterfaceList);

		[DllImport("wlanapi.dll", EntryPoint = "WlanFreeMemory")]
		public static extern void WlanFreeMemory([In] IntPtr pMemory);

		[DllImport("wlanapi.dll", SetLastError = true)]
		public static extern uint WlanGetAvailableNetworkList(
			IntPtr hClientHandle,
			ref Guid pInterfaceGuid,
			uint dwFlags,
			IntPtr pReserved,
			ref IntPtr ppAvailableNetworkList);

		[DllImport("wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern uint WlanGetProfile(IntPtr hClientHandle,
			ref Guid pInterfaceGuid,
			string strProfileName,
			IntPtr pReserved,
			ref string pstrProfileXml,
			ref uint pdwFlags,
			ref uint pdwGrantedAccess);

		[DllImport("wlanapi.dll", SetLastError = true)]
		public static extern uint WlanGetProfileList(IntPtr hClientHandle,
			ref Guid pInterfaceGuid,
			IntPtr pReserved,
			ref IntPtr ppProfileList);


		[DllImport("wlanapi.dll", EntryPoint = "WlanHostedNetworkForceStart")]
		public static extern uint WlanHostedNetworkForceStart(
			IntPtr hClientHandle,
			[Out] out WLAN_HOSTED_NETWORK_REASON pFailReason,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", EntryPoint = "WlanHostedNetworkForceStop")]
		public static extern uint WlanHostedNetworkForceStop(
			IntPtr hClientHandle,
			[Out] out WLAN_HOSTED_NETWORK_REASON pFailReason,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", EntryPoint = "WlanHostedNetworkInitSettings")]
		public static extern uint WlanHostedNetworkInitSettings(IntPtr hClientHandle,
			[Out] out WLAN_HOSTED_NETWORK_REASON pFailReason,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", EntryPoint = "WlanHostedNetworkQueryProperty")]
		public static extern uint WlanHostedNetworkQueryProperty(
			IntPtr hClientHandle,
			WLAN_HOSTED_NETWORK_OPCODE OpCode,
			[Out] out uint pDataSize,
			[Out] out IntPtr ppvData,
			[Out] out WLAN_OPCODE_VALUE_TYPE pWlanOpcodeValueType,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", EntryPoint = "WlanHostedNetworkQuerySecondaryKey")]
		public static extern uint WlanHostedNetworkQuerySecondaryKey(
			IntPtr hClientHandle,
			[Out] out uint pKeyLength,
			[Out, MarshalAs(UnmanagedType.LPStr)] out string ppucKeyData,
			[Out] out bool pbIsPassPhrase, [Out] out bool pbPersistent,
			[Out] out WLAN_HOSTED_NETWORK_REASON pFailReason,
			IntPtr pReserved);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern UInt32 WlanHostedNetworkQueryStatus(
			[In] IntPtr hClientHandle,
			[Out] out IntPtr ppWlanHostedNetworkStatus,
			[In, Out] IntPtr pvReserved);

		[DllImport("wlanapi.dll", EntryPoint = "WlanHostedNetworkRefreshSecuritySettings")]
		public static extern uint WlanHostedNetworkRefreshSecuritySettings(
			IntPtr hClientHandle,
			[Out] out WLAN_HOSTED_NETWORK_REASON pFailReason,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", EntryPoint = "WlanHostedNetworkSetProperty")]
		public static extern uint WlanHostedNetworkSetProperty(IntPtr hClientHandle,
			WLAN_HOSTED_NETWORK_OPCODE OpCode,
			uint dwDataSize,
			IntPtr pvData,
			[Out] out WLAN_HOSTED_NETWORK_REASON pFailReason,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", EntryPoint = "WlanHostedNetworkSetSecondaryKey")]
		public static extern uint WlanHostedNetworkSetSecondaryKey(
			IntPtr hClientHandle,
			uint dwKeyLength,
			[In, MarshalAs(UnmanagedType.LPStr)] string pucKeyData,
			bool bIsPassPhrase, bool bPersistent,
			[Out] out WLAN_HOSTED_NETWORK_REASON pFailReason,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", EntryPoint = "WlanHostedNetworkStartUsing")]
		public static extern uint WlanHostedNetworkStartUsing(
			IntPtr hClientHandle,
			[Out] out WLAN_HOSTED_NETWORK_REASON pFailReason,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", EntryPoint = "WlanHostedNetworkStopUsing")]
		public static extern uint WlanHostedNetworkStopUsing(
			IntPtr hClientHandle,
			[Out] out WLAN_HOSTED_NETWORK_REASON pFailReason,
			IntPtr pReserved);


		[DllImport("wlanapi.dll", EntryPoint = "WlanOpenHandle")]
		public static extern uint WlanOpenHandle(
			uint dwClientVersion,
			IntPtr pReserved,
			[Out] out uint pdwNegotiatedVersion,
			ref IntPtr ClientHandle);

		[DllImport("wlanapi.dll", EntryPoint = "WlanQueryInterface")]
		public static extern uint WlanQueryInterface(
			[In] IntPtr hClientHandle,
			[In] ref Guid pInterfaceGuid,
			WLAN_INTF_OPCODE OpCode,
			IntPtr pReserved,
			[Out] out uint pdwDataSize,
			ref IntPtr ppData,
			IntPtr pWlanOpcodeValueType);

		[DllImport("wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern uint WlanReasonCodeToString(
			uint dwReasonCode,
			uint dwBufferSize,
			string pStringBuffer,
			IntPtr pReserved);

		public delegate void WLAN_NOTIFICATION_CALLBACK(
			ref WLAN_NOTIFICATION_DATA notificationData,
			IntPtr context);

		[DllImport("wlanapi.dll", EntryPoint = "WlanRegisterNotification")]
		public static extern uint WlanRegisterNotification(
			IntPtr hClientHandle,
			WLAN_NOTIFICATION_SOURCE dwNotifSource,
			bool bIgnoreDuplicate,
			WLAN_NOTIFICATION_CALLBACK funcCallback,
			IntPtr pCallbackContext,
			IntPtr pReserved,
			[Out] out WLAN_NOTIFICATION_SOURCE pdwPrevNotifSource);

		[DllImport("wlanapi.dll", SetLastError = true)]
		public static extern uint WlanScan(
			IntPtr hClientHandle,
			ref Guid pInterfaceGuid,
			IntPtr pDot11Ssid,
			IntPtr pIeData,
			IntPtr pReserved);

		[DllImport("wlanapi.dll")]
		public static extern uint WlanSetInterface(
			IntPtr hClientHandle,
			ref Guid pInterfaceGuid,
			WLAN_INTF_OPCODE OpCode,
			uint dwDataSize,
			ref object obj,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern uint WlanSetProfile(
			IntPtr hClientHandle,
			ref Guid pInterfaceGuid,
			uint dwFlags,
			string strProfileXml,
			string strAllUserProfileSecurity,
			bool bOverwrite,
			IntPtr pReserved,
			ref uint pdwReasonCode);

		[DllImport("wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern uint WlanSetProfileList(
			IntPtr hClientHandle,
			ref Guid pInterfaceGuid,
			uint dwItems,
			string[] strProfileNames,
			IntPtr pReserved);

		[DllImport("wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern uint WlanSetProfilePosition(
			IntPtr hClientHandle,
			ref Guid pInterfaceGuid,
			string strProfileName,
			uint dwPosition,
			IntPtr pReserved);
	}
}