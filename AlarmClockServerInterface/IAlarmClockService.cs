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

        
    }
}