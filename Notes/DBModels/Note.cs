using System;
using System.Runtime.Serialization;

namespace Notes.DBModels
{
    [DataContract(IsReference = true)]
    public class Note : IDBModel
    {
        #region Fields
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _title;
        [DataMember]
        private string _text;
        [DataMember]
        private DateTime _creationDate;
        [DataMember]
        private DateTime _lastEditDate;
        [DataMember]
        private Guid _ownerGuid;
        [DataMember]
        private Customer _owner;
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

        public string Title
        {
            get
            {
                return _title;
            }
            private set
            {
                // TODO Process lastEditDate
                _title = value;
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            private set
            {
                // TODO Process lastEditDate
                _text = value;
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return _creationDate;
            }
            private set
            {
                _creationDate = value;
            }
        }

        public DateTime LastEditDate
        {
            get
            {
                return _lastEditDate;
            }
            private set
            {
                _lastEditDate = value;
            }
        }

        public Guid OwnerGuid
        {
            get => _ownerGuid;
            set => _ownerGuid = value;
        }

        public virtual Customer Owner
        {
            get => _owner;
            set => _owner = value;
        }
        #endregion

        #region Constructor

        public Note(string title) : this()
        {
            _guid = new Guid();
            _title = title;
            _creationDate = DateTime.Now;
            _lastEditDate = DateTime.Now;
        }

        public Note()
        {

        }
        #endregion

        public override string ToString()
        {
            return $"{_guid} -> ({_title}, {_text})";
        }
    }
}
