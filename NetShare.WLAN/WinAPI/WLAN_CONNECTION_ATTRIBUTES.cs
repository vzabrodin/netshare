using System.Runtime.InteropServices;

namespace NetShare.WLAN.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WLAN_CONNECTION_ATTRIBUTES
	{
		public WLAN_INTERFACE_STATE isState;
		public WLAN_CONNECTION_MODE wlanConnectionMode;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string strProfileMode; //WCHAR[256];

		public WLAN_ASSOCIATION_ATTRIBUTES wlanAssociationAttributes;
		public WLAN_SECURITY_ATTRIBUTES wlanSecurityAttributes;
	}
}