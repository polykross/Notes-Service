using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Notes.DBModels
{
    [DataContract(IsReference = true)]
    public class Customer : IDBModel
    {
        #region Fields
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _firstName;
        [DataMember]
        private string _lastName;
        [DataMember]
        private string _login;
        [DataMember]
        private string _email;
        [DataMember]
        private string _password;
        [DataMember]
        private DateTime _lastLoginDate;
        [DataMember]
        private List<Note> _notes;
        #endregion

        #region Properties
        public Guid Guid
        {
            get => _guid;
            private set => _guid = value;
        }
        public string FirstName
        {
            get => _firstName;
            private set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            private set => _lastName = value;
        }

        public string Login
        {
            get => _login;
            private set => _login = value;
        }

        public string Email
        {
            get => _email;
            private set => _email = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public virtual List<Note> Notes
        {
            get => _notes;
            set => _notes = value;
        }

        public DateTime LastLoginDate
        {
            get => _lastLoginDate;
            private set => _lastLoginDate = value;
        }
        #endregion

        #region Constructor
        public Customer(Guid guid, string firstName, string lastName, string login, string email, string password, DateTime lastLoginDate): this()
        {
            _guid = guid;
            _firstName = firstName;
            _lastName = lastName;
            _login = login;
            _email = email;
            _password = password;
            _lastLoginDate = lastLoginDate;
        }

        public Customer(Guid guid, string firstName, string lastName, string login, string email, string password) : this()
        {
            _guid = guid;
            _firstName = firstName;
            _lastName = lastName;
            _login = login;
            _email = email;
            _password = password;
        }

        public Customer(string firstName, string lastName, string login, string email, string password) : this()
        {
            _guid = Guid.NewGuid();
            _firstName = firstName;
            _lastName = lastName;
            _login = login;
            _email = email;
            _password = password;
        }

        public Customer()
        {
            _notes = new List<Note>();
        }
        #endregion

        public void AddNote(Note note)
        {
            note.OwnerGuid = Guid;
            note.Owner = this;
            Notes.Add(note);
        }

        public bool CheckPassword(string password)
        {
            return _password.Equals(password);
        }

        public override string ToString()
        {
            var notesGuidsString = $"[ {string.Join(" ,", Notes.Select(n => n.Guid).ToList())} ]";
            return "Customer: { " +
                   $"Login: {Login}, " +
                   $"Password: {Password}, " +
                   $"FirstName: {FirstName}, " +
                   $"LastName: {LastName}, " +
                   $"LastName: {LastName}, " +
                   $"LastLoginDate: {LastLoginDate}, " +
                   $"NotesGuids: {notesGuidsString}" +
            " }";
        }
    }
}
