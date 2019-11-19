using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Notes.DBModels
{
    [DataContract]
    public class Customer : IDBModel
    {
        #region Fields
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _login;
        [DataMember]
        private string _password;
        [DataMember]
        private List<Note> _notes;
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

        public string Password
        {
            get
            {
                return _password;
            }
            set { _password = value; }
        }
        public virtual List<Note> Notes
        {
            get => _notes;
            set => _notes = value;
        }
        #endregion

        #region Constructor

        public Customer(string login, string password) : this()
        {
            _guid = Guid.NewGuid();
            _login = login;
            SetPassword(password);
        }

        public Customer()
        {
            _notes = new List<Note>();
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
            return $"{_guid} -> ({_login}, {_password})";
        }
    }
}
