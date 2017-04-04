using System;
using System.Runtime.InteropServices;

namespace NetShare.Wlan.WinAPI
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WLAN_INTERFACE_INFO_LIST
	{
		public int dwNumberOfItems;
		public int dwIndex;
		public WLAN_INTERFACE_INFO[] InterfaceInfo;

		public WLAN_INTERFACE_INFO_LIST(IntPtr pList)
		{
			dwNumberOfItems = Marshal.ReadInt32(pList, 0);
			dwIndex = Marshal.ReadInt32(pList, 4);
			InterfaceInfo = new WLAN_INTERFACE_INFO[dwNumberOfItems];
			for (int i = 0; i < dwNumberOfItems; i++)
			{
				IntPtr pItemList = new IntPtr(pList.ToInt32() + i * 532 + 8);
				WLAN_INTERFACE_INFO wii = new WLAN_INTERFACE_INFO();
				wii = (WLAN_INTERFACE_INFO)Marshal.PtrToStructure(pItemList, typeof(WLAN_INTERFACE_INFO));
				InterfaceInfo[i] = wii;
			}
		}
	}
}
