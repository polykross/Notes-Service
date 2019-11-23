using System;
using System.Runtime.Serialization;

namespace Notes.CommunicationContract
{
    [DataContract]
    public class CustomerDTO
    {
        #region Fields
        [DataMember(Name = "Guid")]
        private Guid _guid;
        [DataMember(Name = "Login")]
        private string _login;
        [DataMember(Name = "Password")]
        private string _password;
        [DataMember(Name = "FirstName")]
        private string _firstName;
        [DataMember(Name = "LastName")]
        private string _lastName;
        [DataMember(Name = "Email")]
        private string _email;
        [DataMember(Name = "LastLoginDate")]
        private DateTime _lastLoginDate;
        #endregion

        #region Properties
        public Guid Guid
        {
            get => _guid;
            set => _guid = value;
        }

        public string Login
        {
            get => _login;
            set => _login = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }
        /// <summary>
        /// Last login date in UTC
        /// </summary>
        public DateTime LastLoginDate
        {
            get => _lastLoginDate;
            set => _lastLoginDate = value;
        }
        #endregion

        #region Constructor
        public CustomerDTO(string login, string password, string firstName, string lastName, string email)
        {
            _login = login;
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
        }

        public CustomerDTO(Guid guid, string login, string password, string firstName, string lastName, string email, DateTime lastLoginDate)
        {
            _guid = guid;
            _login = login;
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _lastLoginDate = lastLoginDate;
        }
        #endregion

        public override string ToString()
        {
            return "CustomerDTO: { " + 
                   $"login: {Login}, " +
                   $"password: {Password}, " +
                   $"firstName: {FirstName}, " +
                   $"lastName: {LastName}, " +
                   $"email: {LastName}, " +
                   $"lastLoginDate: {LastLoginDate}" + 
            " }";
        }
    }
}
