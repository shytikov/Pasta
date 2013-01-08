using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Configuration;

namespace Basta
{
    [RunInstaller(true)]
    class Setup : Installer
    {
        public Setup()
        {
            var processInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();

            // Set the privileges
            processInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller.DisplayName = ConfigurationManager.AppSettings["name"];
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            // Must be the same as what was set in Program's constructor
            serviceInstaller.ServiceName = ConfigurationManager.AppSettings["name"];

            this.Installers.Add(processInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
