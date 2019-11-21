using Notes.DBModels;
using Notes.DBProviders;
using Notes.DTO;
using Notes.EntityFrameworkDBProvider;
using Notes.Server.WCFServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes.Server.NotesServiceImplementation
{
    public class NotesService : INotesService
    {
        private readonly IDBProvider _provider = new DBProviderDisconnected();
        
        public ServiceCustomerDTO CustomerRegistration(ClientCustomerDTO info)
        {
            var isUnique = _provider.SelectAll<Customer>().All(c => !c.Login.Equals(info.Login));
            if (!isUnique)
            {
                return null;
            }
            var customer = CustomerFromClientDTO(info);
            _provider.Add(customer);
            return ServiceCustomerFromClientDTO(customer.Guid, info);
        }

        public ServiceCustomerDTO UpdateCustomer(Guid guid, ClientCustomerDTO customer)
        {
            _provider.Update(CustomerFromIdAndClientDTO(guid, customer));
            return ServiceCustomerFromClientDTO(guid, customer);
        }

        public void DeleteCustomer(Guid guid)
        {
            _provider.DeleteById<Customer>(guid);
        }

        public ServiceCustomerDTO Login(AuthorizationDTO authorizationInformation)
        {
            var customer = _provider.SelectAll<Customer>().First(c => c.Login.Equals(authorizationInformation.Login));
            return customer.CheckPassword(authorizationInformation.Password) ? GetLoggedInCustomer(customer) : null;
        }

        public IEnumerable<ShortNoteDTO> GetCustomersShortNotes(string login)
        {
            throw new NotImplementedException();
        }

        public NoteDTO AddNote(string login, string title, string text = "")
        {
            throw new NotImplementedException();
        }

        public NoteDTO EditNote(Guid id, string title, string text)
        {
            throw new NotImplementedException();
        }

        public NoteDTO EditNoteTitle(Guid id, string title)
        {
            throw new NotImplementedException();
        }

        public NoteDTO EditNoteText(Guid id, string text)
        {
            throw new NotImplementedException();
        }

        public NoteDTO GetNote(Guid id)
        {
            throw new NotImplementedException();
        }

        #region UtilFunctions
        private static Customer CustomerFromClientDTO(ClientCustomerDTO dto)
        {
            return new Customer(firstName: dto.FirstName, lastName: dto.LastName, login: dto.Login, email: dto.Email, password: dto.Password);
        }

        private static ServiceCustomerDTO ServiceCustomerFromClientDTO(Guid guid, ClientCustomerDTO dto)
        {
            return new ServiceCustomerDTO(guid: guid, login: dto.Login, password: dto.Password, firstName: dto.FirstName, lastName: dto.LastName, email: dto.Email);
        }

        private static Customer CustomerFromIdAndClientDTO(Guid guid, ClientCustomerDTO dto)
        {
            return new Customer(guid: guid, login: dto.Login, password: dto.Password, firstName: dto.FirstName, lastName: dto.LastName, email: dto.Email);
        }

        private static ServiceCustomerDTO GetLoggedInCustomer(Customer customer)
        {
            return new ServiceCustomerDTO(guid: customer.Guid, login: customer.Login, password: customer.Password, firstName: customer.FirstName, lastName: customer.LastName, email: customer.Email, lastLoginDate: DateTime.UtcNow);
        }
        #endregion
    }
}
