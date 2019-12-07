using System;
using System.Collections.Generic;
using System.ServiceModel;
using KMA.APZRP2019.AlarmClock.DBModels;

namespace KMA.APZRP2019.AlarmClock.Server.Interface
{
    [ServiceContract]
    public interface IAlarmClockService
    {
        [OperationContract]
        void AddUser(string firstName, string lastName, string login, string email, string password);

        [OperationContract]
        User GetUser(string login, string password);

        [OperationContract]
        IEnumerable<DBModels.AlarmClock> GetAlarmClocks(Guid userGuid);

        [OperationContract]
        void AddAlarmClock(Guid userGuid, DBModels.AlarmClock clock);

        [OperationContract]
        void DeleteAlarmClock(Guid clockGuid);

        [OperationContract]
        void UpdateAlarmClock(Guid alarmGuid, DateTime lastTime, DateTime nextTime);

    }
}