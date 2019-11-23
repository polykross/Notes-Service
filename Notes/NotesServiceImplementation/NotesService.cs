using Notes.CommunicationContract;
using Notes.DBModels;
using Notes.EntityFrameworkDBProvider;
using Notes.Server.WCFServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes.Server.NotesServiceImplementation
{
    public class NotesService : INotesService
    {
        public CustomerDTO Register(CustomerDTO customer)
        {
            var isUnique = DBProviderUtil.FunctionWithProvider(p => 
                p.Select<Customer>(c => c.Login == customer.Login) == null
            );
            if (!isUnique)
            {
                return null;
            }
            return DBProviderUtil.TryFunctionWithProvider(p =>
            {
                var newCustomer = CreateNewCustomerFromDTO(customer);
                p.Add(newCustomer);
                return CustomerToDTO(newCustomer);
            });
        }

        public CustomerDTO Login(string login, string password)
        {
            return DBProviderUtil.TryFunctionWithProvider(p =>
            {
                var customer = p.Select<Customer>(c => c.Login == login);
                return customer?.CheckPassword(password) == true ? GetLoggedInCustomerDTO(customer) : null;
            });
        }

        public List<ShortNoteDTO> GetNotes(Guid customerGuid)
        {
            return DBProviderUtil.TryFunctionWithProvider(p =>
                p.Select<Customer>(c => c.Guid == customerGuid).Notes.Select(NoteToShortDTO).ToList()
            );
        }

        public NoteDTO GetNote(Guid guid)
        {
            return DBProviderUtil.TryFunctionWithProvider(p =>
            {
                var note = p.Select<Note>(n => n.Guid == guid);
                return note != null ? NoteToDTO(note) : null;
            });
        }

        public NoteDTO AddNote(NoteDTO note, Guid customerGuid)
        {
            return DBProviderUtil.TryFunctionWithProvider(p =>
            {
                var customer = p.Select<Customer>(c => c.Guid == customerGuid);
                if (customer == null)
                {
                    return null;
                }
                var isNoteUnique = p.Select<Note>(n => n.Guid == note.Guid) == null;
                if (!isNoteUnique)
                {
                    return null;
                }
                var newNote = CreateNewNoteFromDTO(note);
                customer.AddNote(newNote);
                p.Update(customer);
                return NoteToDTO(newNote);
            });
        }

        public NoteDTO UpdateNote(NoteDTO note)
        {
            return DBProviderUtil.TryFunctionWithProvider(p =>
            {
                var updateableNote = p.Select<Note>(n => n.Guid == note.Guid);
                if (updateableNote == null)
                {
                    return null;
                }
                updateableNote.Title = note.Title;
                updateableNote.Text = note.Text;
                p.Update(updateableNote);
                return NoteToDTO(updateableNote);
            });
        }

        public bool DeleteNote(Guid guid)
        {
            return DBProviderUtil.TryFunctionWithProvider(p =>
            {
                var note = p.Select<Note>(n => n.Guid == guid);
                if (note == null)
                {
                    return false;
                }
                p.Delete(note);
                return true;
            });
        }

        #region StaticUtilFunctions
        private static Note CreateNewNoteFromDTO(NoteDTO dto)
        {
            return new Note(title: dto.Title, text: dto.Text);
        }

        private static NoteDTO NoteToDTO(Note note)
        {
            return new NoteDTO(guid: note.Guid, title: note.Title, text: note.Text, creationDate: note.CreationDate, lastEditDate: note.LastEditDate);
        }

        private static ShortNoteDTO NoteToShortDTO(Note note)
        {
            return new ShortNoteDTO(guid: note.Guid, title: note.Title);
        }

        private static Customer CreateNewCustomerFromDTO(CustomerDTO dto)
        {
            return new Customer(
                firstName: dto.FirstName,
                lastName: dto.LastName,
                login: dto.Login,
                email: dto.Email,
                password: dto.Password
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
