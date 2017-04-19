﻿using System.Runtime.InteropServices;

namespace NetShare.WLAN.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WLAN_PROFILE_INFO
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string strProfileName;
		public uint dwFlags;
	}
}