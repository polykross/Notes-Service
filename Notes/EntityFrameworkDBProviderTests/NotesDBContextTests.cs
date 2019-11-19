using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.EntityFrameworkDBProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.DBModels;

namespace Notes.EntityFrameworkDBProvider.Tests
{
    [TestClass]
    public class NotesDBContextTests
    {

        private int _freeNumber;

        [TestInitialize]
        [TestCleanup]
        public void RemoveEveryCustomer()
        {
            DeleteCustomers(GetAllCustomers());
        }

        [TestMethod]
        public void CustomersAdditionTest()
        {
            int customersAmount = 3;
            AddCustomers(customersAmount);
            var customers = GetAllCustomers();
            Assert.IsTrue(customers.All(c => c != null) && customers.Count() == customersAmount);
        }

        [TestMethod]
        public void CustomersRemovalTest()
        {
            AddCustomers(10);
            DeleteCustomers(GetAllCustomers());
            Assert.IsTrue(!GetAllCustomers().Any());
        }

        [TestMethod]
        public void CustomersFetchingTest()
        {
            int customersAmount = 10;
            AddCustomers(customersAmount);
            Assert.AreEqual(GetAllCustomers().Count, customersAmount);
        }

        private void DeleteCustomers(IEnumerable<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                DeleteCustomerDisconnected(customer);
            }
        }

        private void DeleteCustomerDisconnected(Customer customer)
        {
            using (var context = new NotesDBContext())
            {
                context.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        private void AddCustomers(int times)
        {
            for (int i = 0; i < times; ++i)
            {
                AddCustomerDisconnected();
            }
        }

        private void AddCustomerDisconnected()
        {
            var customer = GenerateCustomer();
            using (var context = new NotesDBContext())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        private Customer GenerateCustomer()
        {
            return new Customer($"customer{_freeNumber++}", $"{_freeNumber++}");
        }

        private List<Customer> GetAllCustomers()
        {
            using (var context = new NotesDBContext())
            {
                return context.Customers.ToList();
            }
        }
    }
}