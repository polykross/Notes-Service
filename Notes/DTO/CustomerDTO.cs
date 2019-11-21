using System.Runtime.Serialization;

namespace Notes.DTO
{
    [DataContract]
    public class CustomerDTO
    {
        #region Fields
        [DataMember(Name = "login")]
        private string _login;
        [DataMember(Name = "password")]
        private string _password;
        [DataMember(Name = "firstName")]
        private string _firstName;
        [DataMember(Name = "lastName")]
        private string _lastName;
        [DataMember(Name = "email")]
        private string _email;
        #endregion

        #region Properties
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
        #endregion

        #region Constructor
        public CustomerDTO()
        {
        }

        public CustomerDTO(string login, string password, string firstName, string lastName, string email)
        {
            _login = login;
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
        }
        #endregion
    }
}
