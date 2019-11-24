﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using KMA.APZRP2019.AlarmClock.DBModels;

namespace KMA.APZRP2019.AlarmClock.Server.Interface
{
    [ServiceContract]
    public interface IAlarmClockService
    {
        [OperationContract]
        void AddUser(User user);

        [OperationContract]
        User GetUser(string login, string password);

        [OperationContract]
        IEnumerable<DBModels.AlarmClock> GetAlarmClocks(Guid userGuid);

        [OperationContract]
        void AddAlarmClock(Guid userGuid, DBModels.AlarmClock clock);

        [OperationContract]
        void DeleteAlarmClock(Guid clockGuid);

        [OperationContract]
        void UpdateAlarmClock(DBModels.AlarmClock clock);

        [OperationContract]
        void UpdateAllAlarmsByUser(List<DBModels.AlarmClock> clocks);

    }
}