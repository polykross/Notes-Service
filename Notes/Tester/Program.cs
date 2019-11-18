using Notes.EntityFrameworkDBProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using Customer = Notes.EntityFrameworkDBProvider.Customer;

namespace Notes.Tester
{
    class Program
    {
        private static int _freeNumber;

        public static void Main(string[] args)
        {
            DeleteCustomers(GetAllCustomers());
            var result = DBContextTest();
            Console.WriteLine($"DBContextTest:{result}");
            Console.ReadKey();
        }

        private static bool DBContextTest()
        {
            return AdditionStatefulTest1() && RemovalStatefulTest2();
        }

        private static bool RemovalStatefulTest2()
        {
            DeleteCustomers(GetAllCustomers());
            return !GetAllCustomers().Any();
        }

        private static bool AdditionStatefulTest1()
        {
            int customersAmount = 3;
            AddCustomers(customersAmount);
            var customers = GetAllCustomers();
            return customers.All(c => c != null) && customers.Count() == customersAmount;
        }

        private static void DeleteCustomers(IEnumerable<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                DeleteCustomerDisconnected(customer);
            }
        }

        private static void DeleteCustomerDisconnected(Customer customer)
        {
            using (var context = new NotesEntities())
            {
                context.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        private static void AddCustomers(int times)
        {
            for (int i = 0; i < times; ++i)
            {
                AddCustomerDisconnected();
            }
        }

        private static void AddCustomerDisconnected()
        {
            var customer = GenerateCustomer();
            using (var context = new NotesEntities())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        private static Customer GenerateCustomer()
        {
            return new Customer { Id = System.Guid.NewGuid(), Login = $"customer{_freeNumber++}", Password = $"{_freeNumber++}" };
        }

        private static List<Customer> GetAllCustomers()
        {
            using (var context = new NotesEntities())
            {
                return context.Customers.ToList();
            }
        }
    }
}
