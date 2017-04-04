using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using NetShare.Wlan.WinAPI;

namespace NetShare.Wlan
{
	public static class WlanUtils
	{
		[DebuggerStepThrough]
		public static void ThrowOnWin32Error(uint retCode)
		{
			if (retCode != 0) // 0 = ERROR_SUCCESS
			{
				throw new Win32Exception((int)retCode);
			}
		}

		public static string ToString(this DOT11_MAC_ADDRESS mac)
		{
			var sb = new StringBuilder();

			sb.Append(mac.one.ToHexString());
			sb.Append(":");
			sb.Append(mac.two.ToHexString());
			sb.Append(":");
			sb.Append(mac.three.ToHexString());
			sb.Append(":");
			sb.Append(mac.four.ToHexString());
			sb.Append(":");
			sb.Append(mac.five.ToHexString());
			sb.Append(":");
			sb.Append(mac.six.ToHexString());

			return sb.ToString();
		}

		public static string ToString(this DOT11_SSID ssid)
		{
			return ssid.ucSSID;
		}

		public static string ToHexString(this byte value)
		{
			return Convert.ToString(value, 0x10).PadLeft(2, '0');
		}

		public static DOT11_SSID ConvertStringToDOT11_SSID(string ssid)
		{
			return new DOT11_SSID()
			{
				ucSSID = ssid,
				uSSIDLength = (uint)ssid.Length
			};
		}
	}
}