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
           serviceClient = new AlarmClockServiceClient("BasicHttpBinding_IAlarmClockService"); 
        }

        internal void AddUser(User user)
        {
            serviceClient.AddUser(user);
        }

        internal List<User> GetAllUsers()
        {
            try
            {
                return serviceClient.GetAllUsers();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal void AddAlarmClock(Guid userGuid, AlarmClock clock)
        {
            try
            {
               serviceClient.AddAlarmClock(userGuid,clock);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        internal User GetUser(string login, string password)
        {
            try
            {
                return serviceClient.GetUser(login, password);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
