using NetShare.Service.Properties;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;

namespace NetShare.Service
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		static void Main(string[] args)
		{
			if (System.Environment.UserInteractive)
			{
				if (args.Length > 0)
				{
					switch (args[0])
					{
						case "/install":
							InstallService();
							break;
						case "/uninstall":
							UninstallService();
							break;
					}
				}
			}
			else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[]
				{
					new NetShareService()
				};
				ServiceBase.Run(ServicesToRun);
			}
		}

		private static void InstallService()
		{
			UninstallService();
			ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
		}

		private static void UninstallService()
		{
			if (ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == Resources.ServiceName) != null)
			{
				ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
			}
		}
	}
}