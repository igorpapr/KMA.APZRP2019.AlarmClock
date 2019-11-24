using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AlarmClockWPFClient.Annotations;
using AlarmClockWPFClient.Tools;

namespace AlarmClockWPFClient.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {

        private string _login;
        private string _password;
        private string _email;
        private string _name;
        private string _surname;

        private RelayCommand<object> _signUpCommand;
        private RelayCommand<object> _returnCommand;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<Object> SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(
                           o =>
                           {
                               MessageBox.Show(Name + " " + Surname + " " + Email + " " + Login + " " + Password);
                               // TODO make registration to DB and switch to mainView

                           }, o => CanExecuteCommand()));
            }
        }

        public RelayCommand<Object> ReturnCommand
        {
            get
            {
                return _returnCommand ?? (_returnCommand = new RelayCommand<object>(
                    o =>
                    {

                        //TODO make switch to signIn window

                    }));
            }
        }

        public bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password)
                                                      && !string.IsNullOrWhiteSpace(_email) 
                                                      && !string.IsNullOrWhiteSpace(_name)
                                                      && !string.IsNullOrWhiteSpace(_surname);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
