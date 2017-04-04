using System.Runtime.InteropServices;

namespace NetShare.Wlan.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WLAN_SECURITY_ATTRIBUTES
	{
		public bool bSecurityEnabled;
		public bool bOneXEnabled;
		public DOT11_AUTH_ALGORITHM dot11AuthAlgorithm;
		public DOT11_CIPHER_ALGORITHM dot11CipherAlgorithm;
	}
}