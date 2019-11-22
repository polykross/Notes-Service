using Notes.DTO;
using Notes.Server.NotesServiceImplementation;
using Notes.Server.WCFServerInterface;
using System;
using System.Collections.Generic;

namespace Notes.Server.WCFServerIIS
{
    public class NotesServiceIIS : INotesService
    {
        private readonly INotesService _service = new NotesService();

        public CustomerDTO Register(CustomerDTO customer)
        {
            return _service.Register(customer);
        }

        public CustomerDTO Login(string login, string password)
        {
            return _service.Login(login, password);
        }

        public List<ShortNoteDTO> GetNotes(Guid customerGuid)
        {
            return _service.GetNotes(customerGuid);
        }

        public NoteDTO GetNote(Guid guid)
        {
            return _service.GetNote(guid);
        }

        public NoteDTO AddNote(NoteDTO note, Guid customerGuid)
        {
            return _service.AddNote(note, customerGuid);
        }

        public NoteDTO UpdateNote(NoteDTO note)
        {
            return _service.UpdateNote(note);
        }

        public bool DeleteNote(Guid guid)
        {
            return _service.DeleteNote(guid);
        }
    }
}
