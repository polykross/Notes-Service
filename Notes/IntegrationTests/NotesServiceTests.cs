using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.IntegrationTests.NotesServiceImpl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes.IntegrationTests
{
    [TestClass]
    public class NotesServiceTests
    {
        private NotesServiceClient _client;
        private ServiceTestUtil _util;

        [TestInitialize]
        public void Init()
        {
            _client = new NotesServiceImpl.NotesServiceClient("BasicHttpBinding_INotesService");
            _util = new ServiceTestUtil(_client);
            _util.CleanStorage();
        }

        [TestCleanup]
        public void Close()
        {
            _util.CleanStorage();
            _client.Close();
        }

        [TestMethod]
        public void LoginExistsTest()
        {
            Assert.IsFalse(_client.LoginExists("NonExistentLogin"));
        }

        [TestMethod]
        public void LoginNotExistsTest()
        {
            var builtDTO = _util.BuildUniqueCustomerDTO();
            _client.Register(builtDTO);
            Assert.IsTrue(_client.LoginExists(builtDTO.Login));
        }

        [TestMethod]
        public void CustomersRegistrationTest()
        {
            Assert.IsNotNull(_client.Register(_util.BuildUniqueCustomerDTO()));
        }

        [TestMethod]
        public void CustomersRegistrationFailureTest()
        {
            var builtDTO = _util.BuildUniqueCustomerDTO();
            _client.Register(builtDTO);
            Assert.IsNull(_client.Register(builtDTO));
        }

        [TestMethod]
        public void AuthorizationTest()
        {
            var registered = _client.Register(_util.BuildUniqueCustomerDTO());
            Assert.IsNotNull(registered);
            Assert.IsNotNull(_client.Login(registered.Login, registered.Password));
        }

        [TestMethod]
        public void AuthorizationFailureTest()
        {
            Assert.IsNull(_client.Login(login1: "NonExistingLogin", password: "NonExistingLogin"));
        }

        [TestMethod]
        public void AuthorizationWrongPasswordTest()
        {
            var registered = _client.Register(_util.BuildUniqueCustomerDTO());
            Assert.IsNotNull(registered);
            Assert.IsNull(_client.Login(registered.Login, registered.Password + "q"));
        }

        [TestMethod]
        public void NotesAdditionTest()
        {
            var customer = _util.GetAuthorizedCustomerDTO();
            List<NoteDTO> notesToAdd = new List<NoteDTO> { _util.BuildNoteDTO(), _util.BuildNoteDTO(), _util.BuildNoteDTO()};
            IEnumerable<NoteDTO> addedNotes = notesToAdd.Select(note => _client.AddNote(note, customer.Guid));

            foreach (var note in addedNotes)
            {
                Assert.IsNotNull(_client.GetNote(note.Guid));
            }
        }

        [TestMethod]
        public void NotesAdditionFailureTest()
        {
            var customer = _util.GetAuthorizedCustomerDTO();
            var notesToAdd = new List<NoteDTO> { _util.BuildNoteDTO(), _util.BuildNoteDTO(), _util.BuildNoteDTO()};
            IEnumerable<NoteDTO> addedNotes = notesToAdd.Select(note => _client.AddNote(note, customer.Guid)).ToList();

            foreach (var note in addedNotes)
            {
                Assert.IsNull(_client.AddNote(note, customer.Guid));
            }
        }

        [TestMethod]
        public void GetNoteTest()
        {
            var addedNotes = _util.GetAddedNotes(_util.GetAuthorizedCustomerDTO());
            foreach (var existingNote in addedNotes)
            {
                Assert.IsNotNull(_client.GetNote(existingNote.Guid));
            }
        }

        [TestMethod]
        public void GetNoteFailureTest()
        {
            var removedNotes = _util.GetRemovedNotes(_util.GetAuthorizedCustomerDTO());
            foreach (var existingNote in removedNotes)
            {
                Assert.IsNull(_client.GetNote(existingNote.Guid));
            }
        }

        [TestMethod]
        public void GetNotesTest()
        {
            var customer = _util.GetAuthorizedCustomerDTO();
            IEnumerable<ShortNoteDTO> addedShortNotes = _util.GetAddedNotes(customer)
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
            var customer = _util.GetAuthorizedCustomerDTO();
            IEnumerable<NoteDTO> removedNotes = _util.GetRemovedNotes(customer);
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
            var customer = _util.GetAuthorizedCustomerDTO();
            var notesToAdd = new List<NoteDTO>
            {
                _util.BuildDefaultNoteDTO(),
                _util.BuildDefaultNoteDTO(),
                _util.BuildDefaultNoteDTO()
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
            var customersNotes = _util.GetCustomersNotes(customer).ToList();
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
            var customer = _util.GetAuthorizedCustomerDTO();
            var addedNotes = _util.GetAddedNotes(customer);

            _util.DeleteNotes(addedNotes);
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
            var customer = _util.GetAuthorizedCustomerDTO();
            var addedNotes = _util.GetAddedNotes(customer);

            _util.DeleteNotes(addedNotes);

            var customersNotes = _util.GetCustomersNotes(customer).ToList();
            Assert.IsTrue(customersNotes.Count == 0);
        }

        [TestMethod]
        public void DeleteNoteFailureTest()
        {
            Assert.IsFalse(_client.DeleteNote(Guid.Empty));
        }
    }
}