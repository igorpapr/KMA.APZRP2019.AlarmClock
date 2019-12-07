using AlarmClockWPFClient.ServiceReference1;
using System;
using System.Collections.Generic;
using KMA.APZRP2019.AlarmClock.DBModels;

namespace AlarmClockWPFClient.Tools
{
    //class for providing interface of server
    internal class WCFClientIIS
    {
        private static readonly object Locker = new object();
        private static WCFClientIIS _instance;

        private AlarmClockServiceClient serviceClient;

        internal static WCFClientIIS Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                lock (Locker)
                {
                    return _instance ?? (_instance = new WCFClientIIS());
                }
            }
        }


        private WCFClientIIS()
        {
        }

        internal void Initialize()
        {
            serviceClient = new AlarmClockServiceClient("BasicHttpBinding_IAlarmClockService");
        }


        internal void AddUser(string firstName, string lastName, string login, string email, string password)
        {
            serviceClient.AddUser(firstName, lastName, login, email, password);
        }

        internal List<AlarmClock> GetAlarmClocks(Guid userGuid)
        {
            try
            {
                return serviceClient.GetAlarmClocks(userGuid);
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

        internal void DeleteAlarmClock(Guid alarmGuid)
        {
            try
            {
                serviceClient.DeleteAlarmClock(alarmGuid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal void UpdateAlarmClock(Guid alarmGuid, DateTime lastDateTime, DateTime nextDateTime)
        {
            try
            {
                serviceClient.UpdateAlarmClock(alarmGuid,lastDateTime,nextDateTime);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
