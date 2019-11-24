using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AlarmClockWPFClient.Tools.DataStorage;
using KMA.APZRP2019.AlarmClock.DBModels;

namespace AlarmClockWPFClient.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;

        private static IDataStorage _dataStorage;

        internal static User CurrentUser { get; set; }

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");




            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}
