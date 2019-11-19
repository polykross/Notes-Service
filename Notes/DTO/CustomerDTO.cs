using System.Runtime.Serialization;

namespace Notes.DTO
{
    [DataContract]
    public class CustomerDTO
    {
        [DataMember(Name = "login")]
        private string _login;
        [DataMember(Name = "password")]
        private string _password;

        public CustomerDTO()
        {
        }

        public CustomerDTO(string login, string password)
        {
            _login = login;
            _password = password;
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
            set
            {
                _password = value;
            }
        }
    }
}
