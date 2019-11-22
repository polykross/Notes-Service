using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.DBModels;
using Notes.DBProviders;
using Notes.DTO;
using Notes.EntityFrameworkDBProvider;
using Notes.IntegrationTests.NotesServiceIIS;

namespace Notes.IntegrationTests
{
    [TestClass]
    public class NotesServiceTests
    {
        private int _freeId;
        private NotesServiceClient _client = null;
        private readonly IDBProvider _provider = new DBProviderDisconnected();

        [TestInitialize]
        [TestCleanup]
        public void RemoveEveryCustomer()
        {
            if (_client == null)
            {
                _client = new NotesServiceIIS.NotesServiceClient("BasicHttpBinding_INotesService");
            }
            else
            {
                _client.Close();
            }
            DeleteCustomersDisconnected(GetAllCustomers());
        }

        [TestMethod]
        public void CustomersRegistrationTest()
        {
            ServiceCustomerDTO registered = _client.CustomerRegistration(BuildClientCustomerDTO());
            ServiceCustomerDTO authorized = null;
            if (registered != null)
            {
                authorized = _client.Login(BuildAuthorizationDTO(registered));
            }
            Assert.IsNotNull(authorized);
        }

        [TestMethod]
        public void CustomersRegistrationFailureTest()
        {
            var builtDTO = BuildClientCustomerDTO();
            // First registration - OK
            _client.CustomerRegistration(builtDTO);
            // Second registration with same data - Failure
            Assert.IsNull(_client.CustomerRegistration(builtDTO));
        }



        #region Utils
        private AuthorizationDTO BuildAuthorizationDTO(ServiceCustomerDTO customer)
        {
            var authorization = new AuthorizationDTO();
            authorization.Login = customer.Login;
            authorization.Password = customer.Password;
            return authorization;
        }

        private CustomerDTO BuildClientCustomerDTO()
        {
            var customer = new CustomerDTO();
            customer.Login = $"Login{_freeId}";
            customer.Password = $"Password{_freeId}";
            customer.FirstName = $"FirstName{_freeId}";
            customer.LastName = $"LastName{_freeId}";
            customer.Email = $"Email{_freeId}";
            _freeId++;
            return customer;
        }

        private void DeleteCustomersDisconnected(IEnumerable<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                DeleteCustomerDisconnected(customer);
            }
        }

        private List<Customer> GetAllCustomers()
        {
            return _provider.SelectAll<Customer>().ToList();
        }

        private void DeleteCustomerDisconnected(Customer customer)
        {
            _provider.Delete(customer);
        }
        #endregion
    }
}
