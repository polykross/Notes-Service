using Notes.DBModels;
using System.Collections.Generic;
using System.ServiceModel;

namespace Notes.Server.WCFServerInterface
{
    [ServiceContract]
    public interface INotesService
    {
        [OperationContract]
        IEnumerable<Customer> GetAllCustomers();

        [OperationContract]
        void AddCustomer(Customer customer);
    }
}
