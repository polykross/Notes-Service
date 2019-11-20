using Notes.DBModels;
using Notes.DTO;
using Notes.RESTService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using Notes.DBProviders;
using Notes.EntityFrameworkDBProvider;

namespace Notes.RESTService.Controller
{
    public class CustomerController : ICustomerController
    {
        private readonly CustomerRepository _customerRepository = new CustomerRepository();
        private readonly IDBProvider _dbProvider = new DBProviderDisconnected();

        public List<CustomerDTO> GetAllCustomers()
        {
            List<CustomerDTO> result = new List<CustomerDTO>();
            _dbProvider.SelectAll<Customer>().ToList().ForEach(c => result.Add(new CustomerDTO(c.Login, c.Password)));
            return result;
        }

        public Customer GetCustomer(string id)
        {
            Customer customer;
            if (Guid.TryParse(id, out var newGuid))
            {
                customer = _dbProvider.Select<Customer>(c => c.Guid == newGuid);
            }
            else
            {
                throw new WebFaultException<string>("Incorrect guid format (Guid looks like xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)", HttpStatusCode.BadRequest);
            }

            if (customer == null)
            {
                throw new WebFaultException<string>("No such customer", HttpStatusCode.BadRequest);
            }

            return customer;
        }

        public OperationResult AddCustomer(CustomerDTO customerDto)
        {
            var customer = new Customer(customerDto.Login, customerDto.Password);
            var result = _dbProvider.Add(customer);
            return new OperationResult(result);
        }
    }
}
