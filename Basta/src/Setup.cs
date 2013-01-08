using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Configuration;
using System;

namespace Basta
{
    [RunInstaller(true)]
    public class Setup : Installer
    {
        public Setup()
        {
            var processInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();

            // Set the privileges
            processInstaller.Account = ServiceAccount.LocalSystem;

            var config = ConfigurationManager.OpenExeConfiguration("Basta.exe");

            serviceInstaller.DisplayName = config.AppSettings.Settings["name"].Value;
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            // Must be the same as what was set in Program's constructor
            serviceInstaller.ServiceName = config.AppSettings.Settings["name"].Value;

            this.Installers.Add(processInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
