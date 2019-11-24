using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APZRP2019.AlarmClock.DBModels
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class AlarmClock : IDBModel
    {
        [DataMember]
        private Guid _guid;
        [DataMember]
        private DateTime _lastAlarmTime;
        [DataMember]
        private DateTime _nextAlarmTime;
        [DataMember]
        private User _owner;
        [DataMember]
        private Guid _ownerGuid;


        public Guid Guid
        {
            get => _guid;
            set => _guid = value;
        }

        public DateTime LastAlarmTime
        {
            get => _lastAlarmTime;
            set => _lastAlarmTime = value;
        }

        public DateTime NextAlarmTime
        {
            get => _nextAlarmTime;
            set => _nextAlarmTime = value;
        }

        public virtual User Owner
        {
            get => _owner;
            set => _owner = value;
        }

        public Guid OwnerGuid
        {
            get => _ownerGuid;
            set => _ownerGuid = value;
        }

        public AlarmClock(Guid guid, DateTime lastAlarmTime, DateTime nextAlarmTime) : this()
        {
            _guid = guid;
            _lastAlarmTime = lastAlarmTime;
            _nextAlarmTime = nextAlarmTime;
        }

        public AlarmClock(DateTime lastAlarmTime, DateTime nextAlarmTime) : this()
        {
            _guid = Guid.NewGuid();
            _lastAlarmTime = lastAlarmTime;
            _nextAlarmTime = nextAlarmTime;
        }

        public AlarmClock()
        {

        }
    }
}
