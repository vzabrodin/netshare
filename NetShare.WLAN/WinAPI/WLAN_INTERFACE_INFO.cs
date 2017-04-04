using System;
using System.Runtime.InteropServices;

namespace NetShare.Wlan.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WLAN_INTERFACE_INFO
	{
		public Guid InterfaceGuid;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string strInterfaceDescription; // WCHAR[256]

		public WLAN_INTERFACE_STATE isState;
	}
}
