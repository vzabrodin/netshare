﻿using System.Runtime.InteropServices;

namespace NetShare.WLAN.WinAPI
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct DOT11_MAC_ADDRESS
	{
		public byte one;
		public byte two;
		public byte three;
		public byte four;
		public byte five;
		public byte six;
	}
}