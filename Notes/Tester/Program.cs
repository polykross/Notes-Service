using System;
using System.Collections.Generic;
using System.Linq;
using Notes.DTO;
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
                ServiceCustomerDTO registered = serverClient.CustomerRegistration(new ClientCustomerDTO("login1", "password1", "firstName1",
                    "lastName1", "email1"));
                if (registered != null)
                {
                    ServiceCustomerDTO authorized = serverClient.Login(new AuthorizationDTO(registered.Login, registered.Password));
                }
            }
            finally
            {
                serverClient?.Close();
            }
        }
        
    }
}
