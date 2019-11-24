using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AlarmClockWPFClient.Annotations;
using AlarmClockWPFClient.Tools;
using AlarmClockWPFClient.Tools.Managers;
using KMA.APZRP2019.AlarmClock.DBModels;

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

        private Thread _workingThread1;
        private readonly CancellationToken _token;
        private readonly CancellationTokenSource _tokenSource;


        internal MainViewModel()
        {
            List<Alarm> alarms = new List<Alarm>();
            //tmp.Add(new Alarm());
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
                           new RelayCommand<object>(DeleteImplementation, CanExecuteDelete));
            }
        }

        public ICommand EvokeCommand
        {
            get
            {
                return _evokeCommand ?? (_evokeCommand =
                           new RelayCommand<object>(EvokeImplmentation));
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
            //TODO add in DB

//            if (result)
//                NavigationManager.Instance.Navigate(ViewType.Main);

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
                            WCFClientIIS.Instance.DeleteAlarmClock(StationManager.CurrentUser.Guid, SelectedItem.Guid);

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Error while signing in", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }

                        return true;
                    });
                    Alarms.Remove(_selectedItem);
                    LoaderManager.Instance.HideLoader();   
                }
                }
                else
                {
                    MessageBox.Show("Nothing is selected!");
            }
        }

        private void EvokeImplmentation(object obj)
        {
            ProcessManager.RingRing();
            MessageBox.Show("It's time of " + SelectedItem.Time.Hour + ':' + SelectedItem.Time.Minute + " Alarm!!!");
            //TODO update alarm
            
        }

        private void StopImplmentation(object obj)
        {
            ProcessManager.StopRing();
        }

        public bool CanExecuteDelete(object o)
        {
            return _selectedItem != null;
        }

        private void LogoutImplementation(object obj)
        {
            MessageBox.Show("Msssg");

            //TODO switch to log in
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
