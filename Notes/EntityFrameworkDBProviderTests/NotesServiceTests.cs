using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.DBModels;
using Notes.DBProviders;
using Notes.EntityFrameworkDBProvider;
using Notes.IntegrationTests.NotesServiceIIS;
using System.Collections.Generic;
using System.Linq;

namespace Notes.IntegrationTests
{
    [TestClass]
    public class NotesServiceTests
    {
        private int _freeId;
        private NotesServiceClient _client = null;
        private readonly IDBProvider _provider = new DBProvider();

        [TestInitialize]
        [TestCleanup]
        public void Refresh()
        {
            if (_client == null)
            {
                _client = new NotesServiceIIS.NotesServiceClient("BasicHttpBinding_INotesService");
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
            List<NoteDTO> notesToAdd = new List<NoteDTO> { BuildNoteDTO(), BuildNoteDTO(), BuildNoteDTO()};
            IEnumerable<NoteDTO> addedNotes = notesToAdd.Select(note => _client.AddNote(note, customer.Guid)).ToList();

            foreach (var note in addedNotes)
            {
                Assert.IsNull(_client.AddNote(note, customer.Guid));
            }
        }

        #region Utils
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
            return new NoteDTO
            {
                Text = $"Login{_freeId}",
                Title = $"Password{_freeId}"
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
