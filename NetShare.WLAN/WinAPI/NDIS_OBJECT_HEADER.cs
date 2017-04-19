using System.Runtime.InteropServices;

namespace NetShare.WLAN.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct NDIS_OBJECT_HEADER
	{
		public string Type;
		public string Revision;
		public ushort Size;
	}
}