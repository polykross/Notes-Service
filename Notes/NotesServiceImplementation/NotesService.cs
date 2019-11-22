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

        public CustomerDTO Register(CustomerDTO customer)
        {
            var isUnique = _provider.SelectAll<Customer>().All(c => !c.Login.Equals(customer.Login));
            if (!isUnique)
            {
                return null;
            }
            var newCustomer = CustomerFromDTO(customer);
            _provider.Add(newCustomer);
            return CustomerToDTO(newCustomer);
        }

        public CustomerDTO Login(string login, string password)
        {
            var customer = FindByLogin(login);
            return customer?.CheckPassword(password) == true ? GetLoggedInCustomerDTO(customer) : null;
        }

        public List<ShortNoteDTO> GetNotes(Guid customerGuid)
        {
            throw new NotImplementedException();
        }

        public NoteDTO GetNote(Guid guid)
        {
            throw new NotImplementedException();
        }

        public NoteDTO AddNote(NoteDTO note, Guid customerGuid)
        {
            throw new NotImplementedException();
        }

        public NoteDTO UpdateNote(NoteDTO note)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNote(Guid guid)
        {
            throw new NotImplementedException();
        }

        #region UtilFunctions
        private Customer FindByLogin(string login)
        {
            return _provider.SelectAll<Customer>().First(c => c.Login.Equals(login)); ;
        }

        private Customer FindByGuid(Guid guid)
        {
            return _provider.SelectAll<Customer>().First(c => c.Guid.Equals(guid)); ;
        }
        #endregion

        #region StaticUtilFunctions
        private static Customer CustomerFromDTO(CustomerDTO dto)
        {
            return new Customer(
                guid: dto.Guid,
                firstName: dto.FirstName,
                lastName: dto.LastName,
                login: dto.Login,
                email: dto.Email,
                password: dto.Password,
                lastLoginDate: dto.LastLoginDate
            );
        }

        private static CustomerDTO CustomerToDTO(Customer c)
        {
            return new CustomerDTO(
                guid: c.Guid,
                firstName: c.FirstName, 
                lastName: c.LastName, 
                login: c.Login, 
                email: c.Email, 
                password: c.Password,
                lastLoginDate: c.LastLoginDate
                );
        }

        private static CustomerDTO GetLoggedInCustomerDTO(Customer customer)
        {
            return new CustomerDTO(
                guid: customer.Guid, 
                login: customer.Login, 
                password: customer.Password, 
                firstName: customer.FirstName, 
                lastName: customer.LastName, 
                email: customer.Email, 
                lastLoginDate: DateTime.UtcNow
                );
        }
        #endregion
    }
}
