using System;
using System.Runtime.InteropServices;

namespace NetShare.Wlan.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WLAN_HOSTED_NETWORK_STATUS
	{
		public WLAN_HOSTED_NETWORK_STATE HostedNetworkState;
		public Guid IPDeviceID;
		public DOT11_MAC_ADDRESS wlanHostedNetworkBSSID;
		public DOT11_PHY_TYPE dot11PhyType;
		public uint ulChannelFrequency;
		public uint dwNumberOfPeers;
		public IntPtr PeerList; // WLAN_HOSTED_NETWORK_PEER_STATE PeerList[1];
	}
}