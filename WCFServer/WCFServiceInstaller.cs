using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace KMA.APZRP2019.AlarmClock.Server.WCFServer
{
    [RunInstaller(true)]
    public partial class WCFServiceInstaller : System.Configuration.Install.Installer
    {
        public WCFServiceInstaller()
        {
            InitializeComponent();
            _serviceProcessInstaller = new ServiceProcessInstaller();
            _serviceInstaller = new ServiceInstaller();
            _serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            _serviceProcessInstaller.Password = null;
            _serviceProcessInstaller.Username = null;
            _serviceInstaller.ServiceName = AlarmClockWCFService.CurrentServiceName;
            _serviceInstaller.DisplayName = AlarmClockWCFService.CurrentServiceDisplayName;
            _serviceInstaller.Description = AlarmClockWCFService.CurrentServiceDescription;
            _serviceInstaller.StartType = ServiceStartMode.Automatic;
            Installers.AddRange(new Installer[]
            {
                _serviceProcessInstaller,
                _serviceInstaller
            });
        }

        private ServiceProcessInstaller _serviceProcessInstaller;
        private ServiceInstaller _serviceInstaller;
    }
}
