using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClockWPFClient
{
    internal class Alarm
    {
        private DateTime _time;
        private bool _coolDown;

        public Alarm()
        {
            _coolDown = false;
            _time = DateTime.Now.AddMinutes(-1);
        }

        public DateTime Time
        {
            get => _time;
            set => _time = value;
        }

        public bool CoolDown
        {
            get => _coolDown;
            set => _coolDown = value;
        }
    }
}
