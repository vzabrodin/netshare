using System.Runtime.InteropServices;

namespace NetShare.WLAN.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DOT11_BSSID_LIST
	{
		public NDIS_OBJECT_HEADER Header;
		public uint uNumOfEntries;
		public uint uTotalNumOfEntries;
		public DOT11_MAC_ADDRESS[] BSSIDs;
	}
}