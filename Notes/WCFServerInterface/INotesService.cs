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
        /// Register new customer in the system.
        /// </summary>
        /// <param name="customer">Information about customer. Guid and LastLoginDate can be empty.</param>
        /// <returns>
        /// Filled customer DTO if registration was successful, otherwise null.
        /// </returns>
        [OperationContract]
        CustomerDTO Register(CustomerDTO customer);

        /// <summary>
        /// Get information about customer if login and password are correct.
        /// </summary>
        /// <param name="login">Customer's login.</param>
        /// <param name="password">Customer's encrypted password.</param>
        /// <returns>
        /// Customer DTO if login was successful, otherwise null.
        /// </returns>
        [OperationContract]
        CustomerDTO Login(string login, string password);

        /// <summary>
        /// Get a list of Notes (with only Guid and Title) for customer with specified Guid.
        /// </summary>
        /// <param name="customerGuid">Customer's guid.</param>
        /// <returns>
        /// A list of notes.
        /// </returns>
        [OperationContract]
        List<ShortNoteDTO> GetNotes(Guid customerGuid);

        /// <summary>
        /// Get a Note with specified Guid.
        /// </summary>
        /// <param name="guid">Note's guid.</param>
        /// <returns>
        /// A note with specified Guid.
        /// </returns>
        [OperationContract]
        NoteDTO GetNote(Guid guid);

        /// <summary>
        /// Add a note for customer with specified Guid.
        /// </summary>
        /// <param name="note">A note with empty Guid, CreationDate and LastEditDate.</param>
        /// <param name="customerGuid">A Guid of customer to add note to.</param>
        /// <returns>
        /// A note with all fields filled.
        /// </returns>
        [OperationContract]
        NoteDTO AddNote(NoteDTO note, Guid customerGuid);

        /// <summary>
        /// Update a note.
        /// </summary>
        /// <param name="note">Updated note (Guid, CreationDate and LastEditDate are not changed).</param>
        /// <returns>
        /// Updated note.
        /// </returns>
        [OperationContract]
        NoteDTO UpdateNote(NoteDTO note);

        /// <summary>
        /// Delete a note.
        /// </summary>
        /// <param name="guid">A Guid of note to delete.</param>
        /// <returns>
        /// true iff delete successful.
        /// </returns>
        [OperationContract]
        bool DeleteNote(Guid guid);
    }
}
