using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.DBModels;
using Notes.DBProviders;
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
            if (registered != null)
            {
                ServiceCustomerDTO authorized = _client.Login(BuildAuthorizationDTO(registered));
                Assert.IsNotNull(authorized);
            }
            Assert.IsNotNull(registered);
        }

        #region Utils
        private AuthorizationDTO BuildAuthorizationDTO(ServiceCustomerDTO customer)
        {
            var authorization = new AuthorizationDTO();
            authorization.login = customer.login;
            authorization.password = customer.password;
            return authorization;
        }

        private ClientCustomerDTO BuildClientCustomerDTO()
        {
            var customer = new ClientCustomerDTO();
            customer.login = $"Login{_freeId}";
            customer.password = $"Password{_freeId}";
            customer.firstName = $"FirstName{_freeId}";
            customer.lastName = $"LastName{_freeId}";
            customer.email = $"Email{_freeId}";
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
