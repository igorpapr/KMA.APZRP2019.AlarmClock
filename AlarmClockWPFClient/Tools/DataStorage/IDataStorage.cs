using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMA.APZRP2019.AlarmClock.DBModels;

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
