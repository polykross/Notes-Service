using Notes.DBModels;
using Notes.EntityFrameworkDBProvider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes.WCFRESTService
{
    public class CustomerService : ICustomerService
    {
        public List<Customer> GetAllCustomers()
        {
            return FindAllCustomers();
        }

        public Customer GetCustomer(Guid customerId)
        {
            return FindCustomerById(customerId);
        }

        private List<Customer> FindAllCustomers()
        {
            using (var context = GetContext())
            {
                return context.Customers.ToList();
            }
        }

        private Customer FindCustomerById(System.Guid id)
        {
            using (var context = GetContext())
            {
                return (from c in context.Customers
                    where c.Guid == id
                    select c).FirstOrDefault();
                ;
            }
        }

        private NotesDBContext GetContext()
        {
            return new NotesDBContext();
        }
    }
}
