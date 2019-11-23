using Notes.DBModels;
using Notes.EntityFrameworkDBProvider;
using Notes.IntegrationTests.NotesServiceImpl;
using System.Collections.Generic;
using System.Linq;

namespace Notes.IntegrationTests
{
    internal class ServiceTestUtil
    {
        private int _freeId;
        private readonly NotesServiceClient _client;

        public ServiceTestUtil(NotesServiceClient client)
        {
            _client = client;
        }

        public void CleanStorage()
        {
            DeleteCustomers(GetAllCustomers());
        }

        public void DeleteNotes(IEnumerable<NoteDTO> notes)
        {
            foreach (var note in notes)
            {
                _client.DeleteNote(note.Guid);
            }
        }

        public List<NoteDTO> GetCustomersNotes(CustomerDTO customer)
        {
            return _client.GetNotes(customerGuid: customer.Guid).Select(sn => _client.GetNote(sn.Guid)).ToList();
        }

        public List<NoteDTO> GetRemovedNotes(CustomerDTO customer)
        {
            var addedNotes = GetAddedNotes(customer).ToList();
            foreach (var addedNote in addedNotes)
            {
                _client.DeleteNote(addedNote.Guid);
            }
            return addedNotes;
        }

        public List<NoteDTO> GetAddedNotes(CustomerDTO customer)
        {
            var notesToAdd = new List<NoteDTO> { BuildNoteDTO(), BuildNoteDTO(), BuildNoteDTO() };
            return notesToAdd.Select(note => _client.AddNote(note, customer.Guid)).ToList();
        }

        public CustomerDTO GetAuthorizedCustomerDTO()
        {
            var registered = _client.Register(BuildUniqueCustomerDTO());
            return _client.Login(registered.Login, registered.Password);
        }

        public CustomerDTO BuildUniqueCustomerDTO()
        {
            var customer = new CustomerDTO
            {
                Login = $"Login{_freeId}",
                Password = $"Password{_freeId}",
                FirstName = $"FirstName{_freeId}",
                LastName = $"LastName{_freeId}",
                Email = $"Email{_freeId}"
            };
            _freeId++;
            return customer;
        }

        public NoteDTO BuildNoteDTO()
        {
            return BuildNoteDTO(title: $"Title{_freeId}", text: $"Text{_freeId}");
        }

        public NoteDTO BuildDefaultNoteDTO()
        {
            return BuildNoteDTO(title: $"Default", text: $"Default");
        }

        public NoteDTO BuildNoteDTO(string title, string text)
        {
            return new NoteDTO
            {
                Text = title,
                Title = text
            };
        }

        public void DeleteCustomers(IEnumerable<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                DeleteCustomer(customer);
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            DBProviderUtil.ActionWithProvider(provider => provider.Delete(customer));
        }

        public List<Customer> GetAllCustomers()
        {
            return DBProviderUtil.FunctionWithProvider(
                provider => provider.SelectAll<Customer>().ToList()
            );
        }
    }
}
