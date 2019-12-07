using AlarmClockWPFClient.Annotations;
using AlarmClockWPFClient.Navigation;
using AlarmClockWPFClient.Tools;
using AlarmClockWPFClient.Tools.Managers;
using KMA.APZRP2019.AlarmClock.DBModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AlarmClockWPFClient.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Alarm> _alarms;
        private Alarm _selectedItem;

        private ICommand _addCommand;
        private ICommand _deleteCommand;
        private ICommand _evokeCommand;
        private ICommand _stopCommand;
        private ICommand _logoutCommand;
        private ICommand _saveCommand;

        private Thread _workingThread1;
        private readonly CancellationToken _token;
        private readonly CancellationTokenSource _tokenSource;


        internal MainViewModel()
        {
            List<Alarm> alarms = new List<Alarm>();
            List<AlarmClock> tmp = WCFClientIIS.Instance.GetAlarmClocks(StationManager.CurrentUser.Guid);

            foreach (var alrm in tmp)
            {
                alarms.Add(new Alarm(alrm.Guid,alrm.NextAlarmTime));   
            }

            _alarms = new ObservableCollection<Alarm>(alarms);

            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            StartWorkingThread();
            ProcessManager.StopThreads += StopWorkingThread;
        }

        private void StartWorkingThread()
        {
            _workingThread1 = new Thread(WorkingThreadProcess1);
            _workingThread1.Start();
        }

        private void WorkingThreadProcess1()
        {
            while (!_token.IsCancellationRequested)
            {
                foreach (var t in Alarms)
                {
                    if (ProcessManager.CheckAlarm(t))
                    {
                        t.CoolDown = true;
                        MessageBox.Show("It's time of " + t.Time.Hour + ':' + t.Time.Minute + " Alarm!!!");
                    }
                }

                Thread.Sleep(1000);
                if (_token.IsCancellationRequested)
                    break;

                for (int i = 0; i < Alarms.Count; i++)
                {
                    if (Alarms[i].CoolDown
                        && Alarms[i].Time.Hour == DateTime.Now.Hour
                        && Alarms[i].Time.Minute == DateTime.Now.Minute - 1)
                    {
                        Alarms[i].CoolDown = false;
                    }
                }
            }
        }

        private void StopWorkingThread()
        {
            _tokenSource.Cancel();
            _workingThread1.Join(100);
            _workingThread1.Abort();
            _workingThread1 = null;
        }

        public Alarm SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }


        public ObservableCollection<Alarm> Alarms
        {
            get => _alarms;
            set
            {
                _alarms = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand =
                           new RelayCommand<object>(AddImplementation));
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand =
                           new RelayCommand<object>(DeleteImplementation, CanExecuteCommand));
            }
        }

        public ICommand EvokeCommand
        {
            get
            {
                return _evokeCommand ?? (_evokeCommand =
                           new RelayCommand<object>(EvokeImplementation, CanExecuteCommand));
            }
        }

        public ICommand StopCommand
        {
            get
            {
                return _stopCommand ?? (_stopCommand =
                           new RelayCommand<object>(StopImplmentation));
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand =
                           new RelayCommand<object>(LogoutImplementation));
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand =
                           new RelayCommand<object>(SaveImplementation));
            }
        }


        private async void SaveImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    List<AlarmClock> alrmClocks = new List<AlarmClock>();
                    foreach (var al in Alarms)
                    {
                        alrmClocks.Add(new AlarmClock(al.Guid,al.Time,al.Time));
                    }
                    WCFClientIIS.Instance
                        .UpdateAllClocks(alrmClocks);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error while signing in", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            });
            //Alarms.Add(new Alarm());
            LoaderManager.Instance.HideLoader();

        }

        private async void AddImplementation(object obj)
        {
           
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    WCFClientIIS.Instance
                        .AddAlarmClock(StationManager.CurrentUser.Guid,new AlarmClock(DateTime.Now.AddMinutes(-1), DateTime.Now.AddMinutes(-1)));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error while signing in", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                
                return true;
            });
            Alarms.Add(new Alarm());
            LoaderManager.Instance.HideLoader();
        }

        private async void DeleteImplementation(object obj)
        {
            if (_selectedItem != null)
            {
                if (MessageBox.Show("Are you sure you want to delete "
                                    + _selectedItem.Time.Hour + ":" + _selectedItem.Time.Minute + " Alarm?",
                        "Question",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    LoaderManager.Instance.ShowLoader();
                    var result = await Task.Run(() =>
                    {
                        try
                        {
                            WCFClientIIS.Instance.DeleteAlarmClock(SelectedItem.Guid);

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Server error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }

                        return true;
                    });
                    if (result)
                    { 
                        Alarms.Remove(_selectedItem);
                    }
                    LoaderManager.Instance.HideLoader();   
                }
            }
            else
            {
                MessageBox.Show("Nothing is selected!");
            }
        }

        private async void EvokeImplementation(object obj)
        {
            ProcessManager.RingRing();
            MessageBox.Show("It's time of " + SelectedItem.Time.Hour + ':' + SelectedItem.Time.Minute + " Alarm!!!");
            await Task.Run(() =>
            {
                try
                {
                    WCFClientIIS.Instance.UpdateAlarmClock(new AlarmClock(SelectedItem.Guid,DateTime.Now, SelectedItem.Time));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error while signing in", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            });
        }

        private void StopImplmentation(object obj)
        {
            ProcessManager.StopRing();
        }

        public bool CanExecuteCommand(object o)
        {
            return _selectedItem != null;
        }

        private void LogoutImplementation(object obj)
        {
            if (MessageBox.Show("Are you sure you want to log out?",
                    "Question",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                NavigationManager.Instance.Navigate(ViewType.SignIn);
                try
                {
                    File.Delete(FileFolderHelper.StorageFilePath);
                }
                catch (IOException)
                {}
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
