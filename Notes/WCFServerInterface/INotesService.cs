using System.Collections.Generic;
using System.ServiceModel;
using Notes.DTO;

namespace Notes.Server.WCFServerInterface
{
    [ServiceContract]
    public interface INotesService
    {
        [OperationContract]
        IEnumerable<CustomerDTO> GetAllCustomers();

        [OperationContract]
        void AddCustomer(CustomerDTO customer);
    }
}
