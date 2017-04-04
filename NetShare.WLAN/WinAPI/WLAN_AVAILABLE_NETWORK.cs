using System.Runtime.InteropServices;

namespace NetShare.Wlan.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WLAN_AVAILABLE_NETWORK
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string strProfileName; // WCHAR[256]

		public DOT11_SSID dot11Ssid;
		public DOT11_BSS_TYPE dot11BssType;
		public uint uNumberOfBssids;
		public bool bNetworkConnectable;
		public uint wlanNotConnectableReason; // WLAN_REASON_CODE -> DWORD
		public uint uNumberOfPhyTypes;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public DOT11_PHY_TYPE[] dot11PhyTypes;

		public bool bMorePhyTypes;
		public uint wlanSignalQuality;
		public bool bSecurityEnabled;
		public DOT11_AUTH_ALGORITHM dot11DefaultAuthAlgorithm;
		public DOT11_CIPHER_ALGORITHM dot11DefaultCipherAlgorithm;
		public uint dwFlags;
		public uint dwReserved;
	}
}
