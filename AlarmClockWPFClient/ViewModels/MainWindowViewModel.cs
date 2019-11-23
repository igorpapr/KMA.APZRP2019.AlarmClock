using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using AlarmClockWPFClient.Annotations;
using AlarmClockWPFClient.Tools;

namespace AlarmClockWPFClient.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Alarm> _alarms;

        private ICommand _addCommand;
        private ICommand _deleteCommand;
        private ICommand _evokeCommand;
        private ICommand _logoutCommand;

        public MainWindowViewModel()
        {
            List<Alarm> tmp = new List<Alarm>();
            for (int i = 0; i < 7; i++)
            tmp.Add(new Alarm());
            _alarms = new ObservableCollection<Alarm>(tmp);
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
                           new RelayCommand<object>(DeleteImplementation));
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

        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = 
                           new RelayCommand<object>(LogoutImplementation));
            }
        }


        private void AddImplementation(object obj)
        {
            MessageBox.Show("Msssg");
        }

        private void DeleteImplementation(object obj)
        {
            MessageBox.Show("Msssg");
        }

        private void EvokeImplmentation(object obj)
        {
            MessageBox.Show("Msssg");
        }

        private void LogoutImplementation(object obj)
        {
            MessageBox.Show("Msssg");
        }



        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
