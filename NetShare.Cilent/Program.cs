using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetShare.Cilent
{
	static class Program
	{
		static Mutex mutex = new Mutex(true, "NetShareClient");
		/// <summary>)
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (mutex.WaitOne(TimeSpan.Zero, true))
			{
				Application.Run(new FormMain());
			}
			else
			{
				MessageBox.Show("Only one instance at a time");
			}
		}
	}
}