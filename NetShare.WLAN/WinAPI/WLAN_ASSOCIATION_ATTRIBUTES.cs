using System.Runtime.InteropServices;

namespace NetShare.Wlan.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WLAN_ASSOCIATION_ATTRIBUTES
	{
		public DOT11_SSID dot11Ssid;
		public DOT11_BSS_TYPE dot11BssType;
		public DOT11_MAC_ADDRESS dot11Bssid;
		public DOT11_PHY_TYPE dot11PhyType;
		public uint uDot11PhyIndex;
		public uint wlanSignalQuality; //WLAN_SIGNAL_QUALITY -> ULONG
		public uint ulRxRate;
		public uint ulTxRate;
	}
}
