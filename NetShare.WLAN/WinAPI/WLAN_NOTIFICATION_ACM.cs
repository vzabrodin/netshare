﻿namespace NetShare.Wlan.WinAPI
{
	public enum WLAN_NOTIFICATION_ACM
	{
		START,
		AUTOCONF_ENABLED,
		AUTOCONF_DISABLED,
		BACKGROUND_SCAN_ENABLED,
		BACKGROUND_SCAN_DISABLED,
		BSS_TYPE_CHANGE,
		POWER_SETTING_CHANGE,
		SCAN_COMPLETE,
		SCAN_FAIL,
		CONNECTION_START,
		CONNECTION_COMPLETE,
		CONNECTION_ATTEMPT_FAIL,
		FILTER_LIST_CHANGE,
		INTERFACE_ARRIVAL,
		INTERFACE_REMOVAL,
		PROFILE_CHANGE,
		PROFILE_NAME_CHANGE,
		PROFILES_EXHAUSTED,
		NETWORK_NOT_AVAILABLE,
		NETWORK_AVAILABLE,
		DISCONNECTING,
		DISCONNECTED,
		ADHOC_NETWORK_STATE_CHANGE,
	}
}