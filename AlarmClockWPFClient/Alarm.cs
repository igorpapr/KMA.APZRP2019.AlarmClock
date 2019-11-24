using System;

namespace AlarmClockWPFClient
{
    internal class Alarm
    {
        private Guid _guid;
        private DateTime _time;
        private bool _coolDown;

        public Alarm()
        {
            _coolDown = false;
            _time = DateTime.Now.AddMinutes(-1);
        }

        public Alarm(Guid guid, DateTime time)
        {
            _coolDown = false;
            _time = time;
            _guid = guid;
        }

        public Guid Guid
        {
            get => _guid;
            private set { _guid = value; }
        }

        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                _coolDown = false;
            } 
        }

        public bool CoolDown
        {
            get => _coolDown;
            set => _coolDown = value;
        }
    }
}
