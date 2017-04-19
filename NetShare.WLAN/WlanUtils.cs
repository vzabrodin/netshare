using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using NetShare.WLAN.WinAPI;

namespace NetShare.WLAN
{
	public static class WlanUtils
	{
		/// <summary>
		/// Throws an exception if returned code of Win32 function is unsuccessfull
		/// </summary>
		/// <param name="retCode">returned code</param>
		[DebuggerStepThrough]
		public static void ThrowOnWin32Error(uint retCode)
		{
			if (retCode != 0) // 0 = ERROR_SUCCESS
			{
				throw new Win32Exception((int)retCode);
			}
		}

		/// <summary>
		/// Converts MAC address to string
		/// </summary>
		/// <param name="mac">MAC address</param>
		/// <returns>string representation of MAC address</returns>
		public static string ConvertToString(this DOT11_MAC_ADDRESS mac)
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

		/// <summary>
		/// Converts byte value to HEX string
		/// </summary>
		/// <param name="value">byte value</param>
		/// <returns>HEX string</returns>
		public static string ToHexString(this byte value)
		{
			return Convert.ToString(value, 0x10).PadLeft(2, '0');
		}

		/// <summary>
		/// Converts string representation of SSID to DOT11_SSID structure
		/// </summary>
		/// <param name="ssid"></param>
		/// <returns></returns>
		public static DOT11_SSID ToDOT11_SSID(this string ssid)
		{
			return new DOT11_SSID()
			{
				ucSSID = ssid,
				uSSIDLength = (uint)ssid.Length
			};
		}
	}
}