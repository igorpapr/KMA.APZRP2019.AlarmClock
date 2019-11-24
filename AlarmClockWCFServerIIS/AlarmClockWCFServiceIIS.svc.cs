using System;
using System.Collections.Generic;
using KMA.APZRP2019.AlarmClock.DBModels;
using KMA.APZRP2019.AlarmClock.Server.AlarmClockServiceImpl;
using KMA.APZRP2019.AlarmClock.Server.Interface;

namespace KMA.APZRP2019.AlarmClock.Server.AlarmClockWCFServerIIS
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class AlarmClockWCFServiceIIS : IAlarmClockService
    {
        private AlarmClockImpl service = new AlarmClockImpl();

        public void AddUser(User user)
        {
            service.AddUser(user);
        }

        public User GetUser(string login, string password)
        {
            return service.GetUser(login, password);
        }

        public IEnumerable<DBModels.AlarmClock> GetAlarmClocks(Guid userGuid)
        {
            return service.GetAlarmClocks(userGuid);
        }

        public void AddAlarmClock(Guid userGuid, DBModels.AlarmClock clock)
        {
            service.AddAlarmClock(userGuid, clock);
        }

        public void DeleteAlarmClock(Guid alarmGuid)
        {
            service.DeleteAlarmClock(alarmGuid);
        }

        public void UpdateAlarmClock(DBModels.AlarmClock clock)
        {
            service.UpdateAlarmClock(clock);
        }

        public void UpdateAllAlarmsByUser(List<DBModels.AlarmClock> clocks)
        {
            service.UpdateAllAlarmsByUser(clocks);
        }
    }
}
