using System.Runtime.Serialization;

namespace Notes.DTO
{
    [DataContract]
    public class OperationResultDTO
    {
        #region Fields
        [DataMember(Name = "result")]
        private bool _result;
        #endregion

        #region Properties
        public bool Result
        {
            get => _result;
            set => _result = value;
        }
        #endregion

        #region Constructor
        public OperationResultDTO()
        {
        }

        public OperationResultDTO(bool result)
        {
            _result = result;
        }
        #endregion
    }
}
