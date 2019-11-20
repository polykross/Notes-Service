using Notes.DBModels;
using Notes.DBProviders;
using Notes.DTO;
using Notes.EntityFrameworkDBProvider;
using Notes.Server.WCFServerInterface;
using System.Collections.Generic;
using System.Linq;

namespace Notes.Service.NotesServiceImplementation
{
    public class NotesService : INotesService
    {
        private readonly IDBProvider _provider = new DBProviderDisconnected();

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            return _provider.SelectAll<Customer>().Select(c => new CustomerDTO(c.Login, c.Password));
        }

        public void AddCustomer(CustomerDTO customer)
        {
            _provider.Add(new Customer(customer.Login, customer.Password));
        }
    }
}
