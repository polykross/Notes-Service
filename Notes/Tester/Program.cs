using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Notes.DBModels;
using Notes.EntityFrameworkDBProvider;

namespace Notes.Tester
{
    class Program
    {
        private static int _freeNumber;

        static void Main(string[] args)
        {
            Console.WriteLine($"DBContextTest:{DBContextTest()}");
        }

        private static bool DBContextTest()
        {
            int customersAmount = 3;
            AddCustomers(customersAmount);
            IEnumerable<Customer> customers = GetAllCustomers();
            return customers.All(c => c != null) && customers.Count() == customersAmount;
        }

        private static void AddCustomers(int times)
        {
            using (var context = new NotesDBContext())
            {
                for (int i = 0; i < times; ++i)
                {
                    AddCustomer(context);
                }
            }
        }

        private static void AddCustomer(NotesDBContext context)
        {
            context.Customers.Add(GenerateCustomer());
            context.SaveChanges();
        }

        private static Customer GenerateCustomer()
        {
            return new Customer($"customer{_freeNumber++}", $"{_freeNumber++}");
        }

        private static IEnumerable<Customer> GetAllCustomers()
        {
            using (var context = new NotesDBContext())
            {
                return context.Customers.ToList();
            }
        }
    }
}
