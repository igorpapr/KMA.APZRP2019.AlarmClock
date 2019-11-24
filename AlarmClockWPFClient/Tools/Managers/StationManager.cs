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

        internal static void CloseApp()
        {
            if (MessageBox.Show("Do you really want to shut down?\nAll unsaved data will be lost.\n" +
                                "Save it by using \"Save all\" button before shutting down", "Question",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                StopThreads?.Invoke();
                Environment.Exit(1);
            }    
        }
    }
}
