using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.DBModels;
using Notes.DBProviders;
using Notes.EntityFrameworkDBProvider;
using System.Collections.Generic;
using System.Linq;

namespace Notes.IntegrationTests
{
    [TestClass]
    public class NotesDBContextTests
    {

        private int _freeNumber;
        private readonly IDBProvider _provider = new DBProviderDisconnected();

        [TestInitialize]
        [TestCleanup]
        public void RemoveEveryCustomer()
        {
            DeleteCustomersDisconnected(GetAllCustomers());
        }

        [TestMethod]
        public void CustomersDisconnectedAdditionTest()
        {
            int customersAmount = 3;
            AddCustomersDisconnected(customersAmount);
            var customers = GetAllCustomers();
            Assert.IsTrue(customers.All(c => c != null) && customers.Count() == customersAmount);
        }

        [TestMethod]
        public void CustomersDisconnectedRemovalTest()
        {
            AddCustomersDisconnected(10);
            DeleteCustomersDisconnected(GetAllCustomers());
            Assert.IsTrue(!GetAllCustomers().Any());
        }

        [TestMethod]
        public void CustomersFetchingTest()
        {
            int customersAmount = 10;
            AddCustomersDisconnected(customersAmount);
            Assert.AreEqual(GetAllCustomers().Count, customersAmount);
        }

        [TestMethod]
        public void CustomerDisconnectedEditingTest()
        {
            Customer addedCustomer = AddCustomerDisconnected();
            addedCustomer.Password = "MyNonExistingPassword";
            EditCustomerDisconnected(addedCustomer);
            Customer editedCustomer = GetCustomerById(addedCustomer.Guid);
            Assert.AreEqual(addedCustomer.Password, editedCustomer.Password);
        }

        #region Utils
        private void AddCustomersDisconnected(int times)
        {
            for (int i = 0; i < times; ++i)
            {
                AddCustomerDisconnected();
            }
        }

        private Customer AddCustomerDisconnected()
        {
            var customer = GenerateCustomer();
            _provider.Add(customer);
            return customer;
        }

        private void EditCustomerDisconnected(Customer addedCustomer)
        {
            _provider.Update(addedCustomer);
        }

        private void DeleteCustomersDisconnected(IEnumerable<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                DeleteCustomerDisconnected(customer);
            }
        }

        private void DeleteCustomerDisconnected(Customer customer)
        {
            _provider.Delete(customer);
        }

        private List<Customer> GetAllCustomers()
        {
            return _provider.SelectAll<Customer>().ToList();
        }

        private Customer GetCustomerById(System.Guid id)
        {
            return _provider.Select<Customer>(c => c.Guid.ToString() == id.ToString());
        }

        private Customer GenerateCustomer()
        {
            return new Customer($"customer{_freeNumber++}", $"{_freeNumber}", $"{_freeNumber}", $"{_freeNumber}", $"{_freeNumber}");
        }
        #endregion
    }
}