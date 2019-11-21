using System;
using Notes.DTO;
using Notes.Server.NotesServiceImplementation;
using Notes.Server.WCFServerInterface;
using System.Collections.Generic;

namespace Notes.Server.WCFServerIIS
{
    public class NotesServiceIIS : INotesService
    {
        private readonly INotesService _service = new NotesService();

        public ServiceCustomerDTO CustomerRegistration(ClientCustomerDTO info)
        {
            return _service.CustomerRegistration(info);
        }

        public ServiceCustomerDTO UpdateCustomer(Guid guid, ClientCustomerDTO customer)
        {
            return _service.UpdateCustomer(guid, customer);
        }

        public void DeleteCustomer(Guid guid)
        {
            _service.DeleteCustomer(guid);
        }

        public ServiceCustomerDTO Login(AuthorizationDTO authorizationInformation)
        {
            return _service.Login(authorizationInformation);
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
    }
}
