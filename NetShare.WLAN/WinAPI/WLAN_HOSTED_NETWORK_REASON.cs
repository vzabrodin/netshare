﻿namespace NetShare.WLAN.WinAPI
{
	public enum WLAN_HOSTED_NETWORK_REASON
	{
		SUCCESS,
		UNSPECIFIED,
		PARAMETERS,
		SHUTTING_DOWN,
		INSUFFICIENT_RESOURCES,
		ELEVATION_REQUIRED,
		READ_ONLY,
		PERSISTANCE_FAILED,
		CRYPT_ERROR,
		IMPERSONATION,
		STOP_BEFORE_START,
		INTERFACE_AVAILABLE,
		INTERFACE_UNAVAILABLE,
		MINIPORT_STOPPED,
		MINIPORT_STARTED,
		INCOMPATIBLE_CONNECTION_STARTED,
		INCOMPATIBLE_CONNECTION_STOPPED,
		USER_ACTION,
		CLIENT_ABORT,
		AP_START_FAILED,
		PEER_ARRIVED,
		PEER_DEPARTED,
		PEER_TIMEOUT,
		GP_DENIED,
		SERVICE_UNAVAILABLE,
		DEVICE_CHANGE,
		PROPERTIES_CHANGE,
		VIRTUAL_STATION_BLOCKING_USE,
		SERVICE_AVAILABLE_ON_VIRTUAL_STATION,
	}
}