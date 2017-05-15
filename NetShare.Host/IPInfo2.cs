using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace NetShare.Host
{
	public class IPInfo2
	{
		#region WinAPI import

		private const int MAXLEN_PHYSADDR = 8;

		[StructLayout(LayoutKind.Sequential)]
		private struct MIB_IPNETROW
		{
			[MarshalAs(UnmanagedType.U4)]
			public int dwIndex;
			[MarshalAs(UnmanagedType.U4)]
			public int dwPhysAddrLen;
			[MarshalAs(UnmanagedType.U1)]
			public byte mac0;
			[MarshalAs(UnmanagedType.U1)]
			public byte mac1;
			[MarshalAs(UnmanagedType.U1)]
			public byte mac2;
			[MarshalAs(UnmanagedType.U1)]
			public byte mac3;
			[MarshalAs(UnmanagedType.U1)]
			public byte mac4;
			[MarshalAs(UnmanagedType.U1)]
			public byte mac5;
			[MarshalAs(UnmanagedType.U1)]
			public byte mac6;
			[MarshalAs(UnmanagedType.U1)]
			public byte mac7;
			[MarshalAs(UnmanagedType.U4)]
			public int dwAddr;
			[MarshalAs(UnmanagedType.U4)]
			public int dwType;
		}

		[DllImport("IpHlpApi.dll")]
		[return: MarshalAs(UnmanagedType.U4)]
		private static extern int GetIpNetTable(IntPtr pIpNetTable, [MarshalAs(UnmanagedType.U4)] ref int pdwSize, bool bOrder);

		[DllImport("IpHlpApi.dll", SetLastError = true, CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.U4)]
		private static extern int FreeMibTable(IntPtr plpNetTable);

		private const int ERROR_INSUFFICIENT_BUFFER = 122;
		#endregion

		private string _hostname = string.Empty;

		public string MacAddress { get; private set; }

		public string IPAddress { get; private set; }

		public string HostName
		{
			get
			{
				if (_hostname == string.Empty)
				{
					if (IPAddress != null)
						try
						{
							_hostname = Dns.GetHostEntry(IPAddress).HostName;
						}
						catch (Exception)
						{
						}
				}
				return _hostname;
			}
		}

		public IPInfo2(string mac, string ip)
		{
			MacAddress = mac;
			IPAddress = ip;
		}

		public static IPInfo2 GetIPInfo(string mac)
		{
			IPInfo2 ipinfo = null;
			ipinfo = (from ip in GetIPInfo()
					  where ip.MacAddress.ToLowerInvariant() == mac.ToLowerInvariant() && new Ping().Send(ip.IPAddress).Status == IPStatus.Success
					  select ip).FirstOrDefault();
			return ipinfo;
		}

		public static List<IPInfo2> GetIPInfo()
		{
			int bytesNeeded = 0;
			int result = GetIpNetTable(IntPtr.Zero, ref bytesNeeded, false);
			if (result != ERROR_INSUFFICIENT_BUFFER)
			{
				throw new Win32Exception(result);
			}

			IntPtr buffer = IntPtr.Zero;
			try
			{
				buffer = Marshal.AllocCoTaskMem(bytesNeeded);
				result = GetIpNetTable(buffer, ref bytesNeeded, false);
				if (result != 0)
				{
					throw new Win32Exception(result);
				}

				int count = Marshal.ReadInt32(buffer);
				var currentBuffer = new IntPtr(buffer.ToInt64() + Marshal.SizeOf(typeof(int)));

				var table = new List<IPInfo2>();
				for (int i = 0; i < count; i++)
				{
					MIB_IPNETROW row = (MIB_IPNETROW)Marshal.PtrToStructure(new IntPtr(currentBuffer.ToInt64() + (i * Marshal.SizeOf(typeof(MIB_IPNETROW)))), typeof(MIB_IPNETROW));
					table.Add(new IPInfo2(MACToString(row), new IPAddress(BitConverter.GetBytes(row.dwAddr)).ToString()));
				}

				return table;
			}
			finally
			{
				FreeMibTable(buffer);
			}
		}

		#region Helpers
		private static string MACToString(MIB_IPNETROW row)
		{
			var sb = new StringBuilder();

			sb.Append(ByteToHexString(row.mac0));
			sb.Append(":");
			sb.Append(ByteToHexString(row.mac1));
			sb.Append(":");
			sb.Append(ByteToHexString(row.mac2));
			sb.Append(":");
			sb.Append(ByteToHexString(row.mac3));
			sb.Append(":");
			sb.Append(ByteToHexString(row.mac4));
			sb.Append(":");
			sb.Append(ByteToHexString(row.mac5));

			return sb.ToString();
		}

		private static string ByteToHexString(byte value)
		{
			return Convert.ToString(value, 0x10).PadLeft(2, '0');
		}
		#endregion
	}
}