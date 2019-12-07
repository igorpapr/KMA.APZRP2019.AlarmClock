using System;
using System.Media;

namespace AlarmClockWPFClient.Tools
{
    internal class ProcessManager
    {
        private readonly SoundPlayer player
            = new SoundPlayer(Resource.peetukh1);

        internal bool CheckAlarm(Alarm alarm)
        {
            //if (DateTime.Now.Hour == alarm.Time.Hour && DateTime.Now.Minute == alarm.Time.Minute
            //                                         && !alarm.CoolDown)
            if (DateTime.Now.AddSeconds(-DateTime.Now.Second) == alarm.Time.AddSeconds(-alarm.Time.Second)
                                                     && !alarm.CoolDown)
            {
                RingRing();
                return true;
            }

            return false;

        }

        internal void RingRing()
        {
            player.Load();
            player.Play();
        }

        internal void StopRing()
        {
            player.Stop();
        }

    }
}
