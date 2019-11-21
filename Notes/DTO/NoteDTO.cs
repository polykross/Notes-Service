using System;
using System.Runtime.Serialization;

namespace Notes.DTO
{
    [DataContract]
    public class NoteDTO
    {
        #region Fields
        [DataMember(Name = "guid")]
        private Guid _guid;
        [DataMember(Name = "title")]
        private string _title;
        [DataMember(Name = "text")]
        private string _text;
        [DataMember(Name = "creationDate")]
        private DateTime _creationDate;
        [DataMember(Name = "lastEditDate")]
        private DateTime _lastEditDate;
        #endregion

        #region Properties
        public Guid Guid
        {
            get => _guid;
            set => _guid = value;
        }

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string Text
        {
            get => _text;
            set => _text = value;
        }

        /// <summary>
        /// Creation date in UTC
        /// </summary>
        public DateTime CreationDate
        {
            get => _creationDate;
            set => _creationDate = value;
        }

        /// <summary>
        /// Last edit date in UTC
        /// </summary>
        public DateTime LastEditDate
        {
            get => _lastEditDate;
            set => _lastEditDate = value;
        }
        #endregion

        #region Constructor
        public NoteDTO()
        {
        }

        public NoteDTO(Guid guid, string title, string text, DateTime creationDate, DateTime lastEditDate)
        {
            _guid = guid;
            _title = title;
            _text = text;
            _creationDate = creationDate;
            _lastEditDate = lastEditDate;
        }
        #endregion
    }
}
