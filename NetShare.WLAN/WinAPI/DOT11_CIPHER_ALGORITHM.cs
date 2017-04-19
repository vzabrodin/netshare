namespace NetShare.WLAN.WinAPI
{
	public enum DOT11_CIPHER_ALGORITHM : uint
	{
		NONE = 0x00,
		WEP40 = 0x01,
		TKIP = 0x02,
		CCMP = 0x04,
		WEP104 = 0x05,
		WPA_USE_GROUP = 0x100,
		RSN_USE_GROUP = 0x100,
		WEP = 0x101,
		IHV_START = 0x80000000,
		IHV_END = 0xffffffff,
	}
}