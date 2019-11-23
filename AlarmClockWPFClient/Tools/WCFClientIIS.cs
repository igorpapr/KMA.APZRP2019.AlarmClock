using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AlarmClockWPFClient.ServiceReference1;
using KMA.APZRP2019.AlarmClock.DBModels;

namespace AlarmClockWPFClient.Tools
{
    internal class WCFClientIIS
    {
        private AlarmClockServiceClient serviceClient = null;

        internal WCFClientIIS()
        {
           serviceClient = new AlarmClockServiceClient("BasicHttpBinding_IAlarmClockService2"); 
        }

        internal void AddUser(User user)
        {
            serviceClient.AddUser(user);
        }

    }

}
