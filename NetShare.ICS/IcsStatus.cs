﻿namespace NetShare.ICS
{
	public enum IcsStatus
	{
		DISCONNECTED,
		CONNECTING,
		CONNECTED,
		DISCONNECTING,
		HARDWARE_NOT_PRESENT,
		HARDWARE_DISABLED,
		HARDWARE_MALFUNCTION,
		MEDIA_DISCONNECTED,
		AUTHENTICATING,
		AUTHENTICATION_SUCCEEDED,
		AUTHENTICATION_FAILED,
		INVALID_ADDRESS,
		CREDENTIALS_REQUIRED,
	}
}