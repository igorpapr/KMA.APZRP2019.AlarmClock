﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlarmClockWPFClient.Navigation;
using AlarmClockWPFClient.Tools;
using AlarmClockWPFClient.Tools.DataStorage;
using AlarmClockWPFClient.Tools.Managers;
using AlarmClockWPFClient.ViewModels;
using KMA.APZRP2019.AlarmClock.DBModels;

namespace AlarmClockWPFClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
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
                NavigationManager.Instance.Navigate(ViewType.Main);
                if (tmp.Count <= 0)
                    NavigationManager.Instance.Navigate(ViewType.SignIn);
            }
            catch (Exception)
            {
                NavigationManager.Instance.Navigate(ViewType.SignIn);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            StationManager.CloseApp();
        }

    }
}
