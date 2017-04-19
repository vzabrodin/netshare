using System;
using System.Runtime.InteropServices;

namespace NetShare.WLAN.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WLAN_NOTIFICATION_DATA
	{
		public WLAN_NOTIFICATION_SOURCE NotificationSource;
		public int _NotificationCode;
		public Guid InterfaceGuid;
		public int dwDataSize;
		public IntPtr pData;

		public object NotificationCode
		{
			get
			{
				if (NotificationSource == WLAN_NOTIFICATION_SOURCE.MSM)
					return (WLAN_NOTIFICATION_CODE_MSM)_NotificationCode;
				else if (NotificationSource == WLAN_NOTIFICATION_SOURCE.ACM)
					return (WLAN_NOTIFICATION_ACM)_NotificationCode;
				else
					return _NotificationCode;
			}
		}
	}
}