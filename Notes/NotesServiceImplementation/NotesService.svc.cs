using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Notes.DBModels;
using Notes.DBProviders;
using Notes.DTO;
using Notes.EntityFrameworkDBProvider;
using Notes.Server.WCFServerInterface;

namespace Notes.Server.NotesServiceImplementation
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
