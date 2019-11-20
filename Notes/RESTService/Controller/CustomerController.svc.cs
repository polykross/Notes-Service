using Notes.DBModels;
using Notes.DTO;
using Notes.RESTService.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Web;

namespace Notes.RESTService.Controller
{
    public class CustomerController : ICustomerController
    {
        private readonly CustomerRepository _customerRepository = new CustomerRepository();

        public List<CustomerDTO> GetAllCustomers()
        {
            List<CustomerDTO> result = new List<CustomerDTO>();
            _customerRepository.FindAllCustomers().ForEach(c => result.Add(new CustomerDTO(c.Login, c.Password)));
            return result;
        }

        public Customer GetCustomer(string id)
        {
            Customer customer;
            if (Guid.TryParse(id, out var newGuid))
            {
                customer = _customerRepository.FindCustomerById(newGuid);
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
            var result = _customerRepository.AddCustomer(customerDto.Login, customerDto.Password);
            return new OperationResult(result);
        }
    }
}
