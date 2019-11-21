using System.Runtime.Serialization;

namespace Notes.DTO
{
    [DataContract]
    public class AuthorizationDTO
    {
        #region Fields
        [DataMember(Name = "Login")]
        private string _login;
        [DataMember(Name = "Password")]
        private string _password;
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
        #endregion

        #region Constructor
        public AuthorizationDTO()
        {
        }

        public AuthorizationDTO(string login, string password)
        {
            _login = login;
            _password = password;
        }
        #endregion
    }
}
