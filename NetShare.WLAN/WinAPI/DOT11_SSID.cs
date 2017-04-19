using System.Runtime.InteropServices;

namespace NetShare.WLAN.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct DOT11_SSID
	{
		public uint uSSIDLength;
		
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string ucSSID;
	}
}