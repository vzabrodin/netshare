using System.Runtime.InteropServices;

namespace NetShare.WLAN.WinAPI
{
	[StructLayout(LayoutKind.Sequential)]
	public struct WLAN_HOSTED_NETWORK_RADIO_STATE
	{
		public DOT11_RADIO_STATE dot11SoftwareRadioState;
		public DOT11_RADIO_STATE dot11HardwareRadioState;
	}
}