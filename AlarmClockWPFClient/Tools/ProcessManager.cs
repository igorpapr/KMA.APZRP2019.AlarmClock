using System;
using System.Media;

namespace AlarmClockWPFClient.Tools
{
    internal static class ProcessManager
    {
        private static readonly SoundPlayer player 
            = new SoundPlayer(Resource.peetukh1);

        public static event Action StopThreads;

        internal static bool CheckAlarm(Alarm alarm)
        {
            if (DateTime.Now.Hour == alarm.Time.Hour && DateTime.Now.Minute == alarm.Time.Minute 
                                                     && !alarm.CoolDown)
            {
                RingRing();
                return true;
            }
            return false;
            
        }

        internal static void RingRing()
        {
            player.Load();
            player.Play();
        }

        internal static void StopRing()
        {
            player.Stop();
        }


    //internal static void CloseApp()
    //    {
    //        StopThreads?.Invoke();
    //        Environment.Exit(1);
    //    }
    }
}
