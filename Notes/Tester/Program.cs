using System;
using System.Collections.Generic;
using System.Linq;
using Notes.EntityFrameworkDBProvider;
using Notes.Tester.NotesServiceIIS;
using Customer = Notes.DBModels.Customer;

namespace Notes.Tester
{
    class Program
    {

        public static void Main(string[] args)
        {
            NotesServiceClient serverClient = null;
            try
            {
                serverClient = new NotesServiceIIS.NotesServiceClient("BasicHttpBinding_INotesService");
                var customer = new CustomerDTO {login = "login", password = "password"};
                serverClient.AddCustomer(customer);
                var customers = serverClient.GetAllCustomers();
                customers = serverClient.GetAllCustomers();
            }
            finally
            {
                serverClient?.Close();
            }
        }
        
    }
}
