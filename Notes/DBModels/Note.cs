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
            get => _guid;
            private set => _guid = value;
        }

        public string Title
        {
            get => _title;
            set
            {
                _lastEditDate = DateTime.UtcNow;
                _title = value;
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _lastEditDate = DateTime.UtcNow;
                _text = value;
            }
        }

        public DateTime CreationDate
        {
            get => _creationDate;
            private set => _creationDate = value;
        }

        public DateTime LastEditDate
        {
            get => _lastEditDate;
            private set => _lastEditDate = value;
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
        public Note(string title, string text) : this()
        {
            _guid = Guid.NewGuid();
            _title = title;
            _text = text;
        }

        public Note()
        {
            _creationDate = DateTime.UtcNow;
        }
        #endregion

        public override string ToString()
        {
            return $"{_guid} -> ({_title}, {_text})";
        }
    }
}
