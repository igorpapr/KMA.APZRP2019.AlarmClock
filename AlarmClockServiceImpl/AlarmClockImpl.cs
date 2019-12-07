using KMA.APZRP2019.AlarmClock.DBModels;
using KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper;
using KMA.APZRP2019.AlarmClock.Server.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace KMA.APZRP2019.AlarmClock.Server.AlarmClockServiceImpl
{
    //implementation of server interface
    public class AlarmClockImpl : IAlarmClockService
    {
        public void AddUser(string firstName, string lastName, string login, string email, string password)
        {
            using (var context = new AlarmClockDbContext())
            {
                var resUserLogin = context.Users.FirstOrDefault(u => u.Login == login || u.Email == email);
                if (resUserLogin != null)
                {
                    throw new ArgumentException("User with that login or email already exists");
                }
                context.Users.Add(new User(firstName,lastName,login,email,password));
                context.SaveChanges();
                
            }
        }

        public User GetUser(string login, string password)
        {
            using (var context = new AlarmClockDbContext())
            {
                var resUser = context.Users.FirstOrDefault(u => u.Login == login);
                if (resUser != null)
                {
                    if (!resUser.CheckPassword(password))
                        throw new ArgumentException("Couldn't find user with that login or password");
                }
                else
                {
                    throw new ArgumentException("Couldn't find user with that login or password");
                }
                
                
                return resUser;


            }
        }

        public IEnumerable<DBModels.AlarmClock> GetAlarmClocks(Guid userGuid)
        {
            using (var context = new AlarmClockDbContext())
            {
                return context.AlarmClocks.Where(x=>x.OwnerGuid==userGuid).ToList();

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

        public void DeleteAlarmClock(Guid alarmGuid)
        {
            using (var context = new AlarmClockDbContext())
            {
                var cl = context.AlarmClocks.FirstOrDefault(c => c.Guid == alarmGuid);
                if (cl != null)
                    context.AlarmClocks.Remove(cl);
                else
                {
                    throw new ArgumentException("Couldn't find clock in database: " + alarmGuid);
                }
                context.SaveChanges();
            }
        }

        public void UpdateAlarmClock(Guid alarmGuid, DateTime lastTime, DateTime nextTime)
        {
            using (var context = new AlarmClockDbContext())
            {
                try
                {
                    var cl = context.AlarmClocks.FirstOrDefault(c => c.Guid == alarmGuid);
                    if (cl != null)
                    {
                        cl.LastAlarmTime = lastTime;
                        cl.NextAlarmTime = nextTime;
                        context.SaveChanges();
                    }
                    else throw new ArgumentException("Couldn't find clock in database: ");
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
