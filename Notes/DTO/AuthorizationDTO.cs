using System.Runtime.Serialization;

namespace Notes.DTO
{
    [DataContract]
    class AuthorizationDTO
    {
        #region Fields
        [DataMember]
        private string _login;
        [DataMember]
        private string _password;
        #endregion

        #region Properties
        public string Login => _login;

        public string Password => _password;
        #endregion

        #region Constructor
        public AuthorizationDTO(string login, string password)
        {
            _login = login;
            _password = password;
        }
        #endregion
    }
}
