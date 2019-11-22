﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClockWPFClient
{
    internal class Alarm
    {
        private DateTime _time;

        public Alarm()
        {
            _time = DateTime.Today.AddSeconds(-1);
        }

        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }
    }
}