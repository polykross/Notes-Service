using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.DBModels;
using System.Linq;

namespace Notes.IntegrationTests
{
    [TestClass]
    public class NotesDBContextTests
    {
        private DBContextTestUtil _util;

        [TestInitialize]
        [TestCleanup]
        public void ClearDatabase()
        {
            _util = new DBContextTestUtil();
            _util.DeleteAllCustomers();
        }

        [TestMethod]
        public void CustomersAdditionTest()
        {
            int customersAmount = 3;
            _util.AddCustomers(customersAmount);
            var customers = _util.GetAllCustomers();
            Assert.IsTrue(customers.All(c => c != null) && customers.Count() == customersAmount);
        }

        [TestMethod]
        public void CustomersRemovalTest()
        {
            _util.AddCustomers(10);
            _util.DeleteAllCustomers();
            Assert.IsTrue(!_util.GetAllCustomers().Any());
        }

        [TestMethod]
        public void CustomersFetchingTest()
        {
            int customersAmount = 10;
            _util.AddCustomers(customersAmount);
            Assert.AreEqual(_util.GetAllCustomers().Count, customersAmount);
        }

        [TestMethod]
        public void CustomerEditingTest()
        {
            Customer addedCustomer = _util.AddCustomer();
            addedCustomer.Password = "MyNonExistingPassword";
            _util.EditCustomer(addedCustomer);
            Customer editedCustomer = _util.GetCustomerById(addedCustomer.Guid);
            Assert.AreEqual(addedCustomer.Password, editedCustomer.Password);
        }
    }
}