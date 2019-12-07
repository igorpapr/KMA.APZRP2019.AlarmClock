using AlarmClockWPFClient.Tools.DataStorage;
using KMA.APZRP2019.AlarmClock.DBModels;
using System;
using System.Windows;

namespace AlarmClockWPFClient.Tools.Managers
{
    internal static class StationManager
    {
        private static bool _isMainView;

        public static event Action StopThreads;

        internal static bool isMainView
        {
            get => _isMainView;
            set => _isMainView = value;
        }

        internal static User CurrentUser { get; set; }

        internal static IDataStorage DataStorage { get; private set; }

        internal static void Initialize(IDataStorage dataStorage)
        {
            DataStorage = dataStorage;
        }

        internal static bool CloseApp()
        {
            if (_isMainView && MessageBox.Show("Do you really want to shut down?\nAll unsaved data will be lost.\n" +
                                               "Save it by using \"Save all\" button before shutting down", "Question",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                StopThreads?.Invoke();
                return true;
            }

            return false;
        }
    }
}
