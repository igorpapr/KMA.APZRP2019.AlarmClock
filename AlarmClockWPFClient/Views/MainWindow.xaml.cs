using AlarmClockWPFClient.Navigation;
using AlarmClockWPFClient.Tools;
using AlarmClockWPFClient.Tools.DataStorage;
using AlarmClockWPFClient.Tools.Managers;
using AlarmClockWPFClient.ViewModels;
using KMA.APZRP2019.AlarmClock.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AlarmClockWPFClient
{
    public partial class MainWindow : Window, IContentOwner
    {

        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }

        public MainWindow() 
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            WCFClientIIS.Instance.Initialize();
            try
            {
                List<User> tmp = SerializationManager.Deserialize<List<User>>(FileFolderHelper.StorageFilePath);
                StationManager.CurrentUser = tmp[0];
                if (tmp.Count <= 0)
                {
                    NavigationManager.Instance.Navigate(ViewType.SignIn);
                }
                else
                {
                    NavigationManager.Instance.Navigate(ViewType.Main);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveIntoFile(ex, FileFolderHelper.ExceptionLogFilePath);
                NavigationManager.Instance.Navigate(ViewType.SignIn);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {

            if (!StationManager.CloseApp())
            {
                e.Cancel = true;
            }
                
        }

    }
}
