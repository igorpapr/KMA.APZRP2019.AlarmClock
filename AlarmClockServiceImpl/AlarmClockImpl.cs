using System;
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

        public User GetUser(string login, string password)
        {
            using (var context = new AlarmClockDbContext())
            {
                var resUser = context.Users.Include(i => i.AlarmClocks).FirstOrDefault(u => u.Login == login
                                                                && u.Password == password);
                if (resUser != null)
                {
                    return resUser;
                }
                else
                {
                    throw new ArgumentException("No such user was found or bad credentials");
                }
            }
        }


        public void AddAlarmClock(Guid userGuid, DBModels.AlarmClock clock)
        {
            using (var context = new AlarmClockDbContext())
            {
                var user = context.Users.Include(i => i.AlarmClocks)
                    .FirstOrDefault(u => u.Guid == userGuid);
                if (user != null)
                {
                    user.AlarmClocks.Add(clock);
                    context.SaveChanges();

                }
                else throw new ArgumentException("Couldn't find user with that guid");
            }
        }

        public void DeleteAlarmClock(Guid userGuid, DBModels.AlarmClock clock)
        {
            using (var context = new AlarmClockDbContext())
            {
                var user = context.Users.Include(i => i.AlarmClocks)
                    .FirstOrDefault(u => u.Guid == userGuid);
                if (user != null)
                {
                    var cl = user.AlarmClocks.Remove(clock);
                    context.SaveChanges();

                }
                else throw new ArgumentException("Couldn't find user with that guid");
            }
        }

        public void UpdateAlarmClock(Guid userGuid, DBModels.AlarmClock clock)
        {
            using (var context = new AlarmClockDbContext())
            {
                var user = context.Users.Include(i => i.AlarmClocks)
                    .FirstOrDefault(u => u.Guid == userGuid);
                if (user != null)
                {
                    var cl = user.AlarmClocks.FirstOrDefault(c => c.Guid == clock.Guid);
                    if (cl != null)
                    {
                        cl = clock;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new ArgumentException("Couldn't find clock with that guid");
                    }

                }
                else throw new ArgumentException("Couldn't find user with that guid");
            }
        }
    }
}
