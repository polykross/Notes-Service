using System;
using System.Runtime.Serialization;

namespace Notes.CommunicationContract
{
    [DataContract]
    public class ShortNoteDTO
    {
        #region Fields
        [DataMember(Name = "Guid")]
        private Guid _guid;
        [DataMember(Name = "Title")]
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
        public ShortNoteDTO(Guid guid, string title)
        {
            _guid = guid;
            _title = title;
        }
        #endregion

        public override string ToString()
        {
            return "ShortNoteDTO: { " +
                   $"Guid: {Guid}, " +
                   $"Title: {Title}" +
            " }";
        }
    }
}
