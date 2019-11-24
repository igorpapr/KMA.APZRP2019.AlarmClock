using KMA.APZRP2019.AlarmClock.DBModels;
using System.Collections.Generic;

namespace AlarmClockWPFClient.Tools.DataStorage
{
    internal interface IDataStorage
    {
        bool UserExists(string login);

        User GetUserByLogin(string login);

        void AddUser(User user);
        List<User> UsersList { get; }
    }
}
