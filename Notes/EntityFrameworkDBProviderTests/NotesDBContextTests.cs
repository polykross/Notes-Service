using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.DBModels;
using System.Collections.Generic;
using System.Linq;

namespace Notes.EntityFrameworkDBProvider.Tests
{
    [TestClass]
    public class NotesDBContextTests
    {

        private int _freeNumber;
        private static string testingConnectionStringName = "NotesTestingDB";

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
            addedCustomer.Password = "MyNonExistedPassword";
            using (var context = GetContext())
            {
                context.Entry(addedCustomer).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            Customer editedCustomer = GetCustomerById(addedCustomer.Guid);
            Assert.AreEqual(addedCustomer.Password, editedCustomer.Password);
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
            using (var context = GetContext())
            {
                context.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

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
            using (var context = GetContext())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }

            return customer;
        }

        private Customer GenerateCustomer()
        {
            return new Customer($"customer{_freeNumber++}", $"{_freeNumber++}");
        }

        private List<Customer> GetAllCustomers()
        {
            using (var context = GetContext())
            {
                return context.Customers.ToList();
            }
        }

        private Customer GetCustomerById(System.Guid id)
        {
            using (var context = GetContext())
            {
                return (from c in context.Customers
                    where c.Guid == id
                    select c).FirstOrDefault();
;           }
        }

        private NotesDBContext GetContext()
        {
            return new NotesDBContext(testingConnectionStringName);
        }
    }
}