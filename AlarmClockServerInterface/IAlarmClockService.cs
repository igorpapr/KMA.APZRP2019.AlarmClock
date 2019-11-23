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
        IEnumerable<User> GetAllUsers();

        [OperationContract]
        void AddUser(User user);

        [OperationContract]
        User GetUser(string login, string password);

        //[OperationContract]
        //IEnumerable<DBModels.AlarmClock> GetAlarmClocks(User user);

        [OperationContract]
        void AddAlarmClock(Guid userGuid, DBModels.AlarmClock clock);

        [OperationContract]
        void DeleteAlarmClock(Guid userGuid, DBModels.AlarmClock clock);

        [OperationContract]
        void UpdateAlarmClock(Guid userGuid, DBModels.AlarmClock clock);

    }
}