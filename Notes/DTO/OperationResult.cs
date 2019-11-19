using System.Runtime.Serialization;

namespace Notes.DTO
{
    [DataContract]
    public class OperationResult
    {
        [DataMember(Name = "result")]
        private bool _result;

        public OperationResult(bool result)
        {
            _result = result;
        }

        public bool Result
        {
            get => _result;
            set => _result = value;
        }
    }
}
