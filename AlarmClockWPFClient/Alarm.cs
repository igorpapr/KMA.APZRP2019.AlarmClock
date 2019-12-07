using System;
using System.Threading.Tasks;
using System.Windows;
using AlarmClockWPFClient.Tools;
using AlarmClockWPFClient.Tools.Managers;
using KMA.APZRP2019.AlarmClock.DBModels;

namespace AlarmClockWPFClient
{
    internal class Alarm
    {
        //private Guid _guid;
        //private DateTime _time;
        private bool _coolDown;

        private AlarmClock _alarmClock;

        public Alarm()
        {
            _coolDown = false;
            _alarmClock = new AlarmClock(DateTime.Now.AddMinutes(-1), DateTime.Now.AddMinutes(-1));
        }

        public Alarm(AlarmClock alarmClock)
        {
            _coolDown = false;
            _alarmClock = alarmClock;
        }

        public Guid Guid
        {
            get => _alarmClock.Guid;
            private set => _alarmClock.Guid = value;
        }

        public DateTime Time
        {
            get => _alarmClock.NextAlarmTime;
            set
            {
                _alarmClock.NextAlarmTime = value;
                _coolDown = false;
                UpdateValue();
            }
        }

        public bool CoolDown
        {
            get => _coolDown;
            set => _coolDown = value;
        }

        private async void UpdateValue()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                try
                {
                    WCFClientIIS.Instance
                        .UpdateAlarmClock(_alarmClock.Guid, _alarmClock.LastAlarmTime, _alarmClock.NextAlarmTime);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            LoaderManager.Instance.HideLoader();
        }
    }
}
