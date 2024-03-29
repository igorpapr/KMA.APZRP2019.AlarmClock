﻿using AlarmClockWPFClient.Annotations;
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

        private Thread _workingThread1;
        private readonly CancellationToken _token;
        private readonly CancellationTokenSource _tokenSource;

        private readonly ProcessManager _processManager;



        internal MainViewModel()
        {
            _processManager = new ProcessManager();
            List<Alarm> alarms = new List<Alarm>();
            List<AlarmClock> tmp = WCFClientIIS.Instance.GetAlarmClocks(StationManager.CurrentUser.Guid);

            foreach (var alrm in tmp)
            {
                alarms.Add(new Alarm(alrm));

                if (alarms[alarms.Count-1].Time.Date == DateTime.Today.Date 
                    && alarms[alarms.Count - 1].Time.TimeOfDay < DateTime.Now.TimeOfDay)
                {
                    MessageBox.Show("Skipped one alarm: "
                                    + alarms[alarms.Count - 1].Time.TimeOfDay.Hours+':'
                                    + alarms[alarms.Count - 1].Time.TimeOfDay.Minutes, "Warning", MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    alarms[alarms.Count - 1].Time = alarms[alarms.Count - 1].Time.AddYears(DateTime.Today.Year- alarms[alarms.Count - 1].Time.Year)
                        .AddMonths(DateTime.Today.Month - alarms[alarms.Count - 1].Time.Month)
                        .AddDays(DateTime.Today.Day - alarms[alarms.Count - 1].Time.Day + 1);
                }
            }
            _alarms = new ObservableCollection<Alarm>(alarms);

            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            StartWorkingThread();
            
            StationManager.StopThreads += StopWorkingThread;
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
                    if (_processManager.CheckAlarm(t))
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
            try
            {
                _tokenSource.Cancel();
                _workingThread1.Join(100);
                _workingThread1.Abort();
                _workingThread1 = null;
            }
            catch (Exception ex)
            {
                Logger.SaveIntoFile(ex,FileFolderHelper.ExceptionLogFilePath);
            }
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
                           new RelayCommand<object>(StopImplementation));
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
            AlarmClock newAlarmCock = null;
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    newAlarmCock = new AlarmClock(DateTime.Now, DateTime.Today.AddDays(1));
                    WCFClientIIS.Instance
                        .AddAlarmClock(StationManager.CurrentUser.Guid,newAlarmCock);
                }
                catch (Exception e)
                {
                    Logger.SaveIntoFile(e, FileFolderHelper.ExceptionLogFilePath);
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                
                return true;
            });
            if (result)
            {
                Alarms.Add(new Alarm(newAlarmCock));
            }
            
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
                            Logger.SaveIntoFile(e, FileFolderHelper.ExceptionLogFilePath);
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
            _processManager.RingRing();
            MessageBox.Show("It's time of " + SelectedItem.Time.Hour + ':' + SelectedItem.Time.Minute + " Alarm!!!");
            await Task.Run(() =>
            {
                try
                {
                    WCFClientIIS.Instance.UpdateAlarmClock(SelectedItem.Guid,DateTime.Now, SelectedItem.Time);
                }
                catch (Exception e)
                {
                    Logger.SaveIntoFile(e, FileFolderHelper.ExceptionLogFilePath);
                    MessageBox.Show(e.Message, "Error while signing in", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            });
        }

        private void StopImplementation(object obj)
        {
            _processManager.StopRing();
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
                StationManager.StopAllThreads();
                StationManager.StopThreads -= StopWorkingThread;
                try
                {
                    File.Delete(FileFolderHelper.StorageFilePath);
                }
                catch (IOException ex)
                {
                    Logger.SaveIntoFile(ex, FileFolderHelper.ExceptionLogFilePath);
                    MessageBox.Show("Error while deleting serialized user data.", "Client error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
