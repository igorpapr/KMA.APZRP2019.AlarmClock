using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DBModels
{
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
        private string _password;
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
            private set
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

        public virtual List<AlarmClock> AlarmClocks
        {
            get => _alarmClocks;
            set => _alarmClocks = value;
        }
        #endregion

        #region Constructor

        public User(string firstName, string lastName, string email, string password) : this()
        {
            _guid = Guid.NewGuid();
            _firstName = firstName;
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
            //TODO Add encryption
            _password = password;
        }

        internal bool CheckPassword(string password)
        {
            //TODO Compare encrypted passwords
            return _password == password;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
