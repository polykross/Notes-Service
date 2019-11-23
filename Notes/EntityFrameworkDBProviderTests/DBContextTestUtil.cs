using Notes.DBModels;
using Notes.EntityFrameworkDBProvider;
using System.Collections.Generic;
using System.Linq;

namespace Notes.IntegrationTests
{
    internal class DBContextTestUtil
    {
        private int _freeNumber;

        public void AddCustomers(int times)
        {
            for (int i = 0; i < times; ++i)
            {
                AddCustomer();
            }
        }

        public Customer AddCustomer()
        {
            var customer = GenerateCustomer();
            DBProviderUtil.ActionWithProvider(provider => provider.Add(customer));
            return customer;
        }

        public void EditCustomer(Customer customer)
        {
            DBProviderUtil.ActionWithProvider(provider => provider.Update(customer));
        }

        public void DeleteAllCustomers()
        {
            DeleteCustomers(GetAllCustomers());
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

        public List<Customer> GetAllCustomers()
        {
            return DBProviderUtil.FunctionWithProvider(
                provider => provider.SelectAll<Customer>().ToList()
            );
        }

        public Customer GetCustomerById(System.Guid id)
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
    }
}
