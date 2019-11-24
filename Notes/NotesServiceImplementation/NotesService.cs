using log4net;
using Notes.CommunicationContract;
using Notes.DBModels;
using Notes.EntityFrameworkDBProvider;
using Notes.Logger;
using Notes.Server.WCFServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes.Server.NotesServiceImplementation
{
    public class NotesService : INotesService
    {
        private readonly ILog _logger;

        public NotesService()
        {
            _logger = LoggerHelper.GetLogger(typeof(NotesService));
        }

        public NotesService(ILog logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Check if login exists.
        /// </summary>
        /// <param name="login">Login to check.</param>
        /// <returns>
        /// True iff customer with such login exists in the system.
        /// </returns>
        public bool LoginExists(string login)
        {
            var result = !IsLoginExists(login);
            _logger.Debug($"Customer's login checking: login = {login}, result = {result}");
            return result;
        }

        public CustomerDTO Register(CustomerDTO customer)
        {
            if (!IsLoginExists(customer.Login))
            {
                _logger.Error($"Customer's unsuccessful registration attempt due to not unique login: {customer}");
                return null;
            }

            var result = DBProviderUtil.FunctionWithProvider(p =>
            {
                var newCustomer = CreateNewCustomerFromDTO(customer);
                p.Add(newCustomer);
                return CustomerToDTO(newCustomer);
            });

            if (result == null)
            {
                _logger.Error($"Customer's unsuccessful registration attempt: {customer}");
            }
            else
            {
                _logger.Debug($"Customer's successful registration attempt: {customer}");
            }

            return result;
        }

        public CustomerDTO Login(string login, string password)
        {
            var result = DBProviderUtil.FunctionWithProvider(p =>
            {
                var customer = p.Select<Customer>(c => c.Login == login);
                var isPasswordCorrect = customer?.CheckPassword(password) == true;
                return isPasswordCorrect ? GetLoggedInCustomerDTO(customer) : null;
            });

            if (result == null)
            {
                _logger.Error($"Customer's unsuccessful login attempt: login = {login}, password = {password}");
            }
            else
            {
                _logger.Debug($"Customer's successful login attempt: {result}");
            }

            return result;
        }

        public List<ShortNoteDTO> GetNotes(Guid customerGuid)
        {
            var result = DBProviderUtil.FunctionWithProvider(p =>
                p.Select<Customer>(c => c.Guid == customerGuid).Notes.Select(NoteToShortDTO).ToList()
            );

            if (result == null)
            {
                _logger.Error($"Customer's unsuccessful notes fetching attempt: customerGuid = {customerGuid}");
            }
            else
            {
                _logger.Debug($"Customer's successful notes fetching attempt: customerGuid = {customerGuid}, notes = [ {string.Join(", ", result)} ]");
            }

            return result;
        }

        public NoteDTO GetNote(Guid guid)
        {
            var result = DBProviderUtil.FunctionWithProvider(p =>
            {
                var note = p.Select<Note>(n => n.Guid == guid);
                return note != null ? NoteToDTO(note) : null;
            });

            if (result == null)
            {
                _logger.Error($"Unsuccessful note fetching attempt with guid: {guid}");
            }
            else
            {
                _logger.Debug($"Successful note fetching attempt with guid: {guid}");
            }

            return result;
        }

        public NoteDTO AddNote(NoteDTO note, Guid customerGuid)
        {
            var result = DBProviderUtil.FunctionWithProvider(p =>
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

            if (result == null)
            {
                _logger.Error($"Customer's unsuccessful note addition attempt: customerGuid = {customerGuid}, {note}");
            }
            else
            {
                _logger.Debug($"Customer's successful note fetching attempt: customerGuid = {customerGuid}, {note}");
            }

            return result;
        }

        public NoteDTO UpdateNote(NoteDTO note)
        {
            var result = DBProviderUtil.FunctionWithProvider(p =>
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

            if (result == null)
            {
                _logger.Error($"Customer's unsuccessful note update attempt: {note}");
            }
            else
            {
                _logger.Debug($"Customer's successful note update attempt: {note}");
            }

            return result;
        }

        public bool DeleteNote(Guid guid)
        {
            var result = DBProviderUtil.FunctionWithProvider(p =>
            {
                var note = p.Select<Note>(n => n.Guid == guid);
                if (note == null)
                {
                    return false;
                }
                p.Delete(note);
                return true;
            });

            if (!result)
            {
                _logger.Error($"Customer's unsuccessful note delete attempt: note guid = {guid}");
            }
            else
            {
                _logger.Debug($"Customer's successful note delete attempt: note guid = {guid}");
            }

            return result;
        }

        #region StaticUtilFunctions

        private static bool IsLoginExists(string login)
        {
            var isUnique = DBProviderUtil.FunctionWithProvider(p =>
                p.Select<Customer>(c => c.Login == login) == null
            );
            return isUnique;
        }
        
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
