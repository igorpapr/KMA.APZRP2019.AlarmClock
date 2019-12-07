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

        public void AddUser(User user)
        {
            //TODO перевірити чи нема ще такого юзера, ПЕРЕРОБИТИ
            using (var context = new AlarmClockDbContext())
            {
                var resUserLogin = context.Users.FirstOrDefault(u => u.Login == user.Login || u.Email == user.Email);
                if (resUserLogin != null)
                {
                    throw new ArgumentException("User with that login or email already exists");
                }
                
                context.Users.Add(user);
                context.SaveChanges();
                
            }
        }

        public User GetUser(string login, string password)
        {
            using (var context = new AlarmClockDbContext())
            {
                //TODO переробити md5
                password = MD5.Encrypt(password);
                var resUser = context.Users.FirstOrDefault(u => u.Login == login
                                                                && u.Password == password);
                if (resUser == null)
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

        public void UpdateAllAlarmsByUser(List<DBModels.AlarmClock> clocks)
        {
            using (var context = new AlarmClockDbContext())
            {
                    foreach (var clock in clocks)
                    {
                        //var cl = context.AlarmClocks.FirstOrDefault(c => c.Guid == clock.Guid);
                        var cl = context.AlarmClocks.Find(clock.Guid);
                        if (cl != null)
                        {
                            cl.NextAlarmTime = clock.NextAlarmTime;
                        }
                        else throw new ArgumentException("Couldn't find clock in database.");
                    }
                context.SaveChanges();
            }
        }


        public void UpdateAlarmClock(DBModels.AlarmClock clock)
        {
            using (var context = new AlarmClockDbContext())
            {
                try
                {
                    var cl = context.AlarmClocks.Find(clock.Guid);
                    if (cl != null)
                    {
                        
                        cl.LastAlarmTime = clock.LastAlarmTime;
                        cl.NextAlarmTime = clock.NextAlarmTime;
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
