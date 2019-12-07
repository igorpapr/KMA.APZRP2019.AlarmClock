using AlarmClockWPFClient.Tools.DataStorage;
using KMA.APZRP2019.AlarmClock.DBModels;
using System;
using System.Windows;

namespace AlarmClockWPFClient.Tools.Managers
{
    internal static class StationManager
    {

        public static event Action StopThreads;

        internal static User CurrentUser { get; set; }

        internal static IDataStorage DataStorage { get; private set; }

        internal static void Initialize(IDataStorage dataStorage)
        {
            DataStorage = dataStorage;
        }

        public static void StopAllThreads()
        {
            StopThreads?.Invoke();
        }

        internal static bool CloseApp()
        {
            if (MessageBox.Show("Do you really want to exit?", "Question",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                StopAllThreads();
                return true;
            }

            return false;
        }
    }
}
