using System;
using System.Runtime.Serialization;

namespace Notes.DTO
{
    [DataContract]
    public class ShortNoteDTO
    {
        #region Fields
        [DataMember(Name = "guid")]
        private Guid _guid;
        [DataMember(Name = "title")]
        private string _title;
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
        #endregion

        #region Constructor
        public ShortNoteDTO()
        {
        }

        public ShortNoteDTO(Guid guid, string title)
        {
            _guid = guid;
            _title = title;
        }
        #endregion
    }
}
