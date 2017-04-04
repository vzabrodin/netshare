﻿using System.Runtime.InteropServices;

namespace NetShare.Wlan.WinAPI
{
	[StructLayout(LayoutKind.Sequential)]
	public struct WLAN_HOSTED_NETWORK_PEER_STATE
	{
		public DOT11_MAC_ADDRESS PeerMacAddress;
		public WLAN_HOSTED_NETWORK_PEER_AUTH_STATE PeerAuthState;
	}
}
