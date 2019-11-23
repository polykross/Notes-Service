using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Notes.IntegrationTests
{
    [TestClass]
    public class NotesDBContextTests
    {
        private DBContextTestUtil _util;

        [TestInitialize]
        public void Init()
        {
            _util = new DBContextTestUtil();
            _util.DeleteAllCustomers();
        }

        [TestCleanup]
        public void CleanDatabase()
        {
            _util.DeleteAllCustomers();
        }

        [TestMethod]
        public void CustomersAdditionTest()
        {
            const int customersAmount = 3;
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
            const int customersAmount = 10;
            _util.AddCustomers(customersAmount);
            Assert.AreEqual(_util.GetAllCustomers().Count, customersAmount);
        }

        [TestMethod]
        public void CustomerEditingTest()
        {
            var addedCustomer = _util.AddCustomer();
            addedCustomer.Password = "MyNonExistingPassword";
            _util.EditCustomer(addedCustomer);
            var editedCustomer = _util.GetCustomerById(addedCustomer.Guid);
            Assert.AreEqual(addedCustomer.Password, editedCustomer.Password);
        }
    }
}