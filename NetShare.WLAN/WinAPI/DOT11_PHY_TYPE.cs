namespace NetShare.Wlan.WinAPI
{
	public enum DOT11_PHY_TYPE : uint
	{
		UNKNOWN = 0,
		ANY = 0,
		FHSS,
		DSSS,
		IRBASEBAND,
		OFDM,
		HRDSS,
		ERP,
		HT,
		IHV_START = 0x80000000,
		IHV_END = 0xffffffff,
	}
}