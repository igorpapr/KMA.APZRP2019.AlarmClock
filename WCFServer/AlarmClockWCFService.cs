using System;
using System.ServiceProcess;
using System.ServiceModel;
using KMA.APZRP2019.AlarmClock.Server.AlarmClockServiceImpl;

namespace KMA.APZRP2019.AlarmClock.Server.WCFServer
{
    public partial class AlarmClockWCFService : ServiceBase
    {
        internal const string CurrentServiceName = "AlarmClockService";
        internal const string CurrentServiceDisplayName = "Alarm Clock Service";
        internal const string CurrentServiceSource = "AlarmClockServiceSource";
        internal const string CurrentServiceLogName = "AlarmClockServiceLogName";
        internal const string CurrentServiceDescription = "User Alarm Clock List Simulator for learning purposes.";
        private ServiceHost _serviceHost = null;


        public AlarmClockWCFService()
        {
            InitializeComponent();
            ServiceName = CurrentServiceName;
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //TODO implement Logging
        }

        protected override void OnStart(string[] args)
        {
#if DEBUG
            RequestAdditionalTime(120 * 1000);
#endif
            _serviceHost?.Close();
            try
            {
                _serviceHost = new ServiceHost(typeof(AlarmClockImpl));
                _serviceHost.Open();
            }
            catch (Exception ex)
            {
                //TODO implement Logging
                throw;
            }
        }

        protected override void OnStop()
        {
            RequestAdditionalTime(120 * 1000);
            try
            {
                _serviceHost.Close();
            }
            catch (Exception ex)
            {
                //TODO add Logging
            }
        }
    }
}
