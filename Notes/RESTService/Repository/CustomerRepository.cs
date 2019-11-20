using Notes.DBModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Notes.RESTService.Repository
{
    public class CustomerRepository
    {
        private readonly ContextUtil _contextUtil = new ContextUtil();

        public List<Customer> FindAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            _contextUtil.DoWithContext(ctx => customers.AddRange(ctx.Customers.ToList()));
            return customers;
        }

        public Customer FindCustomerById(System.Guid id)
        {
            Customer customer = null;
            _contextUtil.DoWithContext(ctx =>
                customer = (from c in ctx.Customers 
                            where c.Guid == id 
                            select c).First());
            return customer;
        }

        public bool AddCustomer(string login, string password)
        {
            var result = true;
            Customer customer = new Customer(login, password);
            _contextUtil.DoWithContext(ctx =>
                {
                    try
                    {
                        var isDuplicate = ctx.Customers.Any(c => c.Login == customer.Login || c.Guid == customer.Guid);
                        if (!isDuplicate)
                        {
                            ctx.Entry(customer).State = EntityState.Added;
                            ctx.SaveChanges();
                        }
                        result = !isDuplicate;
                    }
                    catch
                    {
                        result = false;
                        ctx.Dispose();
                    }
                }
            );
            return result;
        }

    }
}