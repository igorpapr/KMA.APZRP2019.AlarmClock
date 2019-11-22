using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using KMA.APZRP2019.AlarmClock.Server.Interface;
using KMA.APZRP2019.AlarmClock.DBModels;
using KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper;

namespace KMA.APZRP2019.AlarmClock.Server.AlarmClockServiceImpl
{
    public class AlarmClockImpl : IAlarmClockService
    {
        public IEnumerable<User> GetAllUsers()
        {
            using (var context = new AlarmClockDbContext())
            {
                return context.Users.Include(u => u.AlarmClocks).ToList();
            }
        }

        public void AddUser(User user)
        {
            using (var context = new AlarmClockDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
