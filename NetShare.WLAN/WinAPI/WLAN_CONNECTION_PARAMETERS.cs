using System.Runtime.InteropServices;

namespace NetShare.WLAN.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WLAN_CONNECTION_PARAMETERS
	{
		public WLAN_CONNECTION_MODE wlanConnectionMode;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string strProfile;

		public DOT11_SSID pDot11Ssid;
		public DOT11_BSSID_LIST pDesiredBssidList;
		public DOT11_BSS_TYPE dot11BssType;
		public uint dwFlags;
	}
}