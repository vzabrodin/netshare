using System.Runtime.InteropServices;

namespace NetShare.Wlan.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct NDIS_OBJECT_HEADER
	{
		public string Type;
		public string Revision;
		public ushort Size;
	}
}