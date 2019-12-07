using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KMA.APZRP2019.AlarmClock.Server.AlarmClockServiceImpl;

namespace KMA.APZRP2019.AlarmClock.DBModels
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class User : IDBModel
    {
        #region Fields
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _firstName;
        [DataMember]
        private string _lastName;
        [DataMember]
        private string _email;
        [DataMember]
        private string _login;
        [DataMember]
        private string _password;
        [DataMember]
        private DateTime _lastLoginTime;
        [DataMember]
        private List<AlarmClock> _alarmClocks;
        #endregion

        #region Properties
        public Guid Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
            }
        }
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            private set
            {
                _firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            private set
            {
                _lastName = value;
            }
        }

        public string Login
        {
            get
            {
                return _login;
            }
            private set
            {
                _login = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            private set
            {
                _email = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set { _password = value; }
        }

        public DateTime LastLoginTime
        {
            get { return _lastLoginTime; }
            set { _lastLoginTime = value; }
        }

        public virtual List<AlarmClock> AlarmClocks
        {
            get => _alarmClocks;
            set => _alarmClocks = value;
        }
        #endregion

        #region Constructor

        public User(string firstName, string lastName, string login, string email, string password) : this()
        {
            _guid = Guid.NewGuid();
            _firstName = firstName;
            _login = login;
            _lastName = lastName;
            _email = email;
            SetPassword(password);
        }

        public User()
        {
            _alarmClocks = new List<AlarmClock>();
        }
        #endregion

        private void SetPassword(string password)
        {
            _password = MD5.Encrypt(password);
        }

        public bool CheckPassword(string password)
        {
            return _password == MD5.Encrypt(password);
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
