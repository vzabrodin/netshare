using System.Runtime.InteropServices;

namespace NetShare.WLAN.WinAPI
{
	[StructLayout(LayoutKind.Sequential)]
	public struct WLAN_HOSTED_NETWORK_SECURITY_SETTINGS
	{
		public DOT11_AUTH_ALGORITHM dot11AuthAlgo;
		public DOT11_CIPHER_ALGORITHM dot11CipherAlgo;
	}
}