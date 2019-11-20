using Notes.DTO;
using Notes.Server.NotesServiceImplementation;
using Notes.Server.WCFServerInterface;
using System.Collections.Generic;

namespace Notes.Server.WCFServerIIS
{
    public class NotesServiceIIS : INotesService
    {
        private readonly INotesService _service = new NotesService();
        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            return _service.GetAllCustomers();
        }

        public void AddCustomer(CustomerDTO customer)
        {
            _service.AddCustomer(customer);
        }
    }
}
