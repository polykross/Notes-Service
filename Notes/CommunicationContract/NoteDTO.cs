using System;
using System.Runtime.Serialization;

namespace Notes.CommunicationContract
{
    [DataContract]
    public class NoteDTO
    {
        #region Fields
        [DataMember(Name = "Guid")]
        private Guid _guid;
        [DataMember(Name = "Title")]
        private string _title;
        [DataMember(Name = "Text")]
        private string _text;
        [DataMember(Name = "CreationDate")]
        private DateTime _creationDate;
        [DataMember(Name = "LastEditDate")]
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
