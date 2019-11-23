using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.Win32;

namespace AlarmClockWPFClient.Tools
{
    internal static class ProcessManager
    {
        private static readonly SoundPlayer player 
            = new SoundPlayer(System.AppDomain.CurrentDomain.BaseDirectory + "\\peetukh.wav");

        public static event Action StopThreads;

        internal static int CheckAlarm(Alarm alarm,int id)
        {
            if (DateTime.Now.Hour == alarm.Time.Hour && DateTime.Now.Minute == alarm.Time.Minute 
                                                     && !alarm.CoolDown)
            {
                RingRing();
                return id;
            }
            else
            {
                return -1;
            }
        }

        private static void RingRing()
        {
            player.Load();
            player.Play();
        }

        internal static void stopRing()
        {
            player.Stop();
        }


    internal static void CloseApp()
        {
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}
