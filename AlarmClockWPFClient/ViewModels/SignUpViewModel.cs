using AlarmClockWPFClient.Annotations;
using AlarmClockWPFClient.Navigation;
using AlarmClockWPFClient.Tools;
using AlarmClockWPFClient.Tools.Managers;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using KMA.APZRP2019.AlarmClock.DBModels;

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
                           SignUpImplementation, CanExecuteCommand));
            }
        }

        public RelayCommand<Object> ReturnCommand
        {
            get
            {
                return _returnCommand ?? (_returnCommand = new RelayCommand<object>(
                    o =>
                    {
                        Name = "";
                        Surname = "";
                        Login = "";
                        Password = "";
                        Email = "";
                        NavigationManager.Instance.Navigate(ViewType.SignIn);
                    }));
            }
        }

        private async void SignUpImplementation(object o)
        {

            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    if (!new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Match(_email).Success)
                    {
                        MessageBox.Show("Email is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else
                    {
                        WCFClientIIS.Instance.AddUser(new User(Name, Surname, Login, Email,Password));
                    }
                   
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error while signing up", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            });
            if (result)
            {
                MessageBox.Show($"Registered new user: {Name} {Surname} {Email}");
                LoaderManager.Instance.HideLoader();
                NavigationManager.Instance.Navigate(ViewType.SignIn);
                Name = "";
                Surname = "";
                Login = "";
                Password = "";
                Email = "";
            }
            else
            {
                LoaderManager.Instance.HideLoader();
            }

        }

        public bool CanExecuteCommand(object o)
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
