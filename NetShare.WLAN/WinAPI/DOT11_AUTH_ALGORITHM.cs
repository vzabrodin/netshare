namespace NetShare.WLAN.WinAPI
{
	public enum DOT11_AUTH_ALGORITHM : uint
	{
		_80211_OPEN			= 1,
		_80211_SHARED_KEY	= 2,
		WPA					= 3,
		WPA_PSK				= 4,
		WPA_NONE			= 5,
		RSNA				= 6,
		RSNA_PSK			= 7,
		IHV_START			= 0x80000000,
		IHV_END				= 0xffffffff,
	}
}