using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.DBModels;
using Notes.EntityFrameworkDBProvider;
using Notes.IntegrationTests.NotesServiceImpl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes.IntegrationTests
{
    [TestClass]
    public class NotesServiceTests
    {
        private int _freeId;
        private NotesServiceClient _client;

        [TestInitialize]
        [TestCleanup]
        public void Refresh()
        {
            if (_client == null)
            {
                _client = new NotesServiceImpl.NotesServiceClient("BasicHttpBinding_INotesService");
            }
            else
            {
                _client.Close();
            }
            DeleteCustomers(GetAllCustomers());
        }

        [TestMethod]
        public void CustomersRegistration()
        {
            Assert.IsNotNull(_client.Register(BuildUniqueCustomerDTO()));
        }

        [TestMethod]
        public void CustomersAuthorization()
        {
            var registered = _client.Register(BuildUniqueCustomerDTO());
            Assert.IsNotNull(registered);
            Assert.IsNotNull(_client.Login(registered.Login, registered.Password));
        }

        [TestMethod]
        public void CustomersRegistrationFailureTest()
        {
            var builtDTO = BuildUniqueCustomerDTO();
            _client.Register(builtDTO);
            Assert.IsNull(_client.Register(builtDTO));
        }

        [TestMethod]
        public void CustomersAuthorizationFailureTest()
        {
            Assert.IsNull(_client.Login(login1: "NonExistingLogin", password: "NonExistingLogin"));
        }

        [TestMethod]
        public void NotesAdditionTest()
        {
            var customer = GetAuthorizedCustomerDTO();
            List<NoteDTO> notesToAdd = new List<NoteDTO> {BuildNoteDTO(), BuildNoteDTO(), BuildNoteDTO()};
            IEnumerable<NoteDTO> addedNotes = notesToAdd.Select(note => _client.AddNote(note, customer.Guid));

            foreach (var note in addedNotes)
            {
                Assert.IsNotNull(_client.GetNote(note.Guid));
            }
        }

        [TestMethod]
        public void NotesAdditionFailureTest()
        {
            var customer = GetAuthorizedCustomerDTO();
            var notesToAdd = new List<NoteDTO> { BuildNoteDTO(), BuildNoteDTO(), BuildNoteDTO()};
            IEnumerable<NoteDTO> addedNotes = notesToAdd.Select(note => _client.AddNote(note, customer.Guid)).ToList();

            foreach (var note in addedNotes)
            {
                Assert.IsNull(_client.AddNote(note, customer.Guid));
            }
        }

        [TestMethod]
        public void GetNoteTest()
        {
            var addedNotes = GetAddedNotes(GetAuthorizedCustomerDTO());
            foreach (var existingNote in addedNotes)
            {
                Assert.IsNotNull(_client.GetNote(existingNote.Guid));
            }
        }

        [TestMethod]
        public void GetNoteFailureTest()
        {
            var removedNotes = GetRemovedNotes(GetAuthorizedCustomerDTO());
            foreach (var existingNote in removedNotes)
            {
                Assert.IsNull(_client.GetNote(existingNote.Guid));
            }
        }

        [TestMethod]
        public void GetNotesTest()
        {
            var customer = GetAuthorizedCustomerDTO();
            IEnumerable<ShortNoteDTO> addedShortNotes = GetAddedNotes(customer)
                    .Select(addedNote => new ShortNoteDTO{Guid = addedNote.Guid, Title = addedNote.Title});
            
            IEnumerable<ShortNoteDTO> customersShortNotes = _client.GetNotes(customer.Guid);

            foreach (var aNote in addedShortNotes)
            {
                var customerHasThisNode =
                    customersShortNotes.Any(note => 
                        note.Guid.Equals(aNote.Guid) && 
                        note.Title.Equals(aNote.Title)
                    );
                Assert.IsTrue(customerHasThisNode);
            }
        }

        [TestMethod]
        public void GetNotesFailureTest()
        {
            var customer = GetAuthorizedCustomerDTO();
            IEnumerable<NoteDTO> removedNotes = GetRemovedNotes(customer);
            IEnumerable<ShortNoteDTO> customersShortNotes = _client.GetNotes(customer.Guid);

            foreach (var aNote in removedNotes)
            {
                var customerHasNoThisNode = customersShortNotes.All(note => !note.Guid.Equals(aNote.Guid));
                Assert.IsTrue(customerHasNoThisNode);
            }
        }

        [TestMethod]
        public void UpdateNoteTest()
        {
            // Initialize notes
            var customer = GetAuthorizedCustomerDTO();
            var notesToAdd = new List<NoteDTO>
            {
                BuildDefaultNoteDTO(),
                BuildDefaultNoteDTO(),
                BuildDefaultNoteDTO()
            };
            var addedNotes = notesToAdd.Select(note => _client.AddNote(note, customer.Guid)).ToList();

            // Update notes
            const string customTextAndTitle = "myCustomTextAndTitle";
            foreach (var aNote in addedNotes)
            {
                aNote.Title = customTextAndTitle;
                aNote.Text = customTextAndTitle;
                _client.UpdateNote(aNote);
            }

            // Check notes
            var customersNotes = GetCustomersNotes(customer).ToList();
            var hasEditedNoteResults = addedNotes.Select(aNote =>
                customersNotes.Any(n => 
                    n.Title.Equals(aNote.Title) && n.Text.Equals(aNote.Text)
                )
            );

            foreach (var hasEditedNote in hasEditedNoteResults)
            {
                Assert.IsTrue(hasEditedNote);
            }

            var hasNoOtherNotes = customersNotes.Count.Equals(addedNotes.Count);
            Assert.IsTrue(hasNoOtherNotes);
        }

        [TestMethod]
        public void UpdateNoteFailureTest()
        {
            var customer = GetAuthorizedCustomerDTO();
            var addedNotes = GetAddedNotes(customer);
            
            DeleteNotes(addedNotes);
            var removedNotes = addedNotes;

            foreach (var rNote in removedNotes)
            {
                rNote.Title = "anything";
                rNote.Text = "anything";
                Assert.IsNull(_client.UpdateNote(rNote));
            }
        }

        [TestMethod]
        public void DeleteNoteTest()
        {
            var customer = GetAuthorizedCustomerDTO();
            var addedNotes = GetAddedNotes(customer);
            
            DeleteNotes(addedNotes);

            var customersNotes = GetCustomersNotes(customer).ToList();
            Assert.IsTrue(customersNotes.Count == 0);
        }

        [TestMethod]
        public void DeleteNoteFailureTest()
        {
            Assert.IsFalse(_client.DeleteNote(Guid.Empty));
        }

        #region Utils

        private void DeleteNotes(IEnumerable<NoteDTO> notes)
        {
            foreach (var note in notes)
            {
                _client.DeleteNote(note.Guid);
            }
        }

        private List<NoteDTO> GetCustomersNotes(CustomerDTO customer)
        {
            return _client.GetNotes(customerGuid: customer.Guid).Select(sn => _client.GetNote(sn.Guid)).ToList();
        }

        private List<NoteDTO> GetRemovedNotes(CustomerDTO customer)
        {
            var addedNotes = GetAddedNotes(customer).ToList();
            foreach (var addedNote in addedNotes)
            {
                _client.DeleteNote(addedNote.Guid);
            }
            return addedNotes;
        }

        private List<NoteDTO> GetAddedNotes(CustomerDTO customer)
        {
            var notesToAdd = new List<NoteDTO> { BuildNoteDTO(), BuildNoteDTO(), BuildNoteDTO() };
            return notesToAdd.Select(note => _client.AddNote(note, customer.Guid)).ToList();
        }

        private CustomerDTO GetAuthorizedCustomerDTO()
        {
            var registered = _client.Register(BuildUniqueCustomerDTO());
            return _client.Login(registered.Login, registered.Password);
        }

        private CustomerDTO BuildUniqueCustomerDTO()
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

        private NoteDTO BuildNoteDTO()
        {
            return BuildNoteDTO(title: $"Title{_freeId}", text: $"Text{_freeId}");
        }

        private NoteDTO BuildDefaultNoteDTO()
        {
            return BuildNoteDTO(title: $"Default", text: $"Default");
        }

        private NoteDTO BuildNoteDTO(string title, string text)
        {
            return new NoteDTO
            {
                Text = title,
                Title = text
            };
        }

        private void DeleteCustomers(IEnumerable<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                DeleteCustomer(customer);
            }
        }

        private void DeleteCustomer(Customer customer)
        {
            DBProviderUtil.ActionWithProvider(provider => provider.Delete(customer));
        }

        private List<Customer> GetAllCustomers()
        {
            return DBProviderUtil.FunctionWithProvider(
                provider => provider.SelectAll<Customer>().ToList()
            );
        }
        #endregion
    }
}