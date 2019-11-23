using System;
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

        [TestInitialize]
        [TestCleanup]
        public void ClearDatabase()
        {
            DeleteCustomers(GetAllCustomers());
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
            DeleteCustomers(GetAllCustomers());
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
            Customer addedCustomer = AddCustomerConnected();
            addedCustomer.Password = "MyNonExistingPassword";
            EditCustomerConnected(addedCustomer);
            Customer editedCustomer = GetCustomerById(addedCustomer.Guid);
            Assert.AreEqual(addedCustomer.Password, editedCustomer.Password);
        }

        #region Utils
        private void AddCustomersDisconnected(int times)
        {
            for (int i = 0; i < times; ++i)
            {
                AddCustomerConnected();
            }
        }

        private Customer AddCustomerConnected()
        {
            var customer = GenerateCustomer();
            DBProviderUtil.ActionWithProvider(provider => provider.Add(customer));
            return customer;
        }

        private void EditCustomerConnected(Customer customer)
        {
            DBProviderUtil.ActionWithProvider(provider => provider.Update(customer));
        }

        private void DeleteCustomers(IEnumerable<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                DeleteCustomer(customer);
            }
        }

        private void DeleteCustomer(Customer customer)
        {
            DBProviderUtil.ActionWithProvider(provider => provider.Delete(customer));
        }

        private List<Customer> GetAllCustomers()
        {
            return DBProviderUtil.FunctionWithProvider(
                provider => provider.SelectAll<Customer>().ToList()
            );
        }

        private Customer GetCustomerById(System.Guid id)
        {
            Customer customer = null;
            DBProviderUtil.ActionWithProvider(provider => 
                customer = provider.Select<Customer>(c => c.Guid.ToString() == id.ToString())
            );
            return customer;
        }

        private Customer GenerateCustomer()
        {
            return new Customer($"customer{_freeNumber++}", $"{_freeNumber}", $"{_freeNumber}", $"{_freeNumber}", $"{_freeNumber}");
        }
        #endregion
    }
}