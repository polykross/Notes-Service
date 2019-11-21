using System;
using System.Collections.Generic;
using System.ServiceModel;
using Notes.DTO;

namespace Notes.Server.WCFServerInterface
{
    [ServiceContract]
    public interface INotesService
    {
        /// <summary>
        /// Registers customer with registration info
        /// </summary>
        /// <returns>Customer's information</returns>
        /// <returns>NULL, if failure occured</returns>
        [OperationContract]
        ServiceCustomerDTO CustomerRegistration(ClientCustomerDTO info);

        /// <summary>
        /// Updates customer with client info
        /// </summary>
        /// <returns>Customer's information</returns>
        /// <returns>NULL, if customer does not exist or other failure occured</returns>
        [OperationContract]
        ServiceCustomerDTO UpdateCustomer(Guid guid, ClientCustomerDTO customer);

        /// <exception>When customer does not exist or other failure occured</exception>
        [OperationContract]
        void DeleteCustomer(Guid guid);

        /// <summary>
        /// Authorizes customer with authorization info.
        /// </summary>
        /// <returns>Customer's information</returns>
        /// <returns>NULL, if authorization is incorrect</returns>
        [OperationContract]
        ServiceCustomerDTO Login(AuthorizationDTO authorizationInformation);

        /// <exception>When customer does not exist or other failure occured</exception>
        [OperationContract]
        IEnumerable<ShortNoteDTO> GetCustomersShortNotes(string login);

        /// <returns>Note information including first creation and last edit date</returns>
        /// <exception>When customer does not exist or other failure occured</exception>
        [OperationContract]
        NoteDTO AddNote(string login, string title, string text = "");

        /// <summary>
        /// Edit note's text and title.
        /// </summary>
        /// <returns>Note information including first creation and last edit date</returns>
        /// <exception>When note does not exist</exception>
        [OperationContract]
        NoteDTO EditNote(Guid id, string title, string text);

        /// <returns>Note information including first creation and last edit date</returns>
        /// <exception>When note does not exist</exception>
        [OperationContract]
        NoteDTO EditNoteTitle(Guid id, string title);

        /// <returns>Note information including first creation and last edit date</returns>
        /// <exception>When note does not exist</exception>
        [OperationContract]
        NoteDTO EditNoteText(Guid id, string text);

        /// <returns>Note information including first creation and last edit date</returns>
        /// <exception>When note does not exist</exception>
        [OperationContract]
        NoteDTO GetNote(Guid id);
    }
}
