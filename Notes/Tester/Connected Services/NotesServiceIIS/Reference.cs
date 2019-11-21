﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Notes.Tester.NotesServiceIIS {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="NotesServiceIIS.INotesService")]
    public interface INotesService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/CustomerRegistration", ReplyAction="http://tempuri.org/INotesService/CustomerRegistrationResponse")]
        Notes.DTO.ServiceCustomerDTO CustomerRegistration(Notes.DTO.ClientCustomerDTO info);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/CustomerRegistration", ReplyAction="http://tempuri.org/INotesService/CustomerRegistrationResponse")]
        System.Threading.Tasks.Task<Notes.DTO.ServiceCustomerDTO> CustomerRegistrationAsync(Notes.DTO.ClientCustomerDTO info);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/UpdateCustomer", ReplyAction="http://tempuri.org/INotesService/UpdateCustomerResponse")]
        Notes.DTO.ServiceCustomerDTO UpdateCustomer(System.Guid guid, Notes.DTO.ClientCustomerDTO customer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/UpdateCustomer", ReplyAction="http://tempuri.org/INotesService/UpdateCustomerResponse")]
        System.Threading.Tasks.Task<Notes.DTO.ServiceCustomerDTO> UpdateCustomerAsync(System.Guid guid, Notes.DTO.ClientCustomerDTO customer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/DeleteCustomer", ReplyAction="http://tempuri.org/INotesService/DeleteCustomerResponse")]
        void DeleteCustomer(System.Guid guid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/DeleteCustomer", ReplyAction="http://tempuri.org/INotesService/DeleteCustomerResponse")]
        System.Threading.Tasks.Task DeleteCustomerAsync(System.Guid guid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/Login", ReplyAction="http://tempuri.org/INotesService/LoginResponse")]
        Notes.DTO.ServiceCustomerDTO Login(Notes.DTO.AuthorizationDTO authorizationInformation);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/Login", ReplyAction="http://tempuri.org/INotesService/LoginResponse")]
        System.Threading.Tasks.Task<Notes.DTO.ServiceCustomerDTO> LoginAsync(Notes.DTO.AuthorizationDTO authorizationInformation);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/GetCustomersShortNotes", ReplyAction="http://tempuri.org/INotesService/GetCustomersShortNotesResponse")]
        Notes.DTO.ShortNoteDTO[] GetCustomersShortNotes(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/GetCustomersShortNotes", ReplyAction="http://tempuri.org/INotesService/GetCustomersShortNotesResponse")]
        System.Threading.Tasks.Task<Notes.DTO.ShortNoteDTO[]> GetCustomersShortNotesAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/AddNote", ReplyAction="http://tempuri.org/INotesService/AddNoteResponse")]
        Notes.DTO.NoteDTO AddNote(string login, string title, string text);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/AddNote", ReplyAction="http://tempuri.org/INotesService/AddNoteResponse")]
        System.Threading.Tasks.Task<Notes.DTO.NoteDTO> AddNoteAsync(string login, string title, string text);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/EditNote", ReplyAction="http://tempuri.org/INotesService/EditNoteResponse")]
        Notes.DTO.NoteDTO EditNote(System.Guid id, string title, string text);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/EditNote", ReplyAction="http://tempuri.org/INotesService/EditNoteResponse")]
        System.Threading.Tasks.Task<Notes.DTO.NoteDTO> EditNoteAsync(System.Guid id, string title, string text);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/EditNoteTitle", ReplyAction="http://tempuri.org/INotesService/EditNoteTitleResponse")]
        Notes.DTO.NoteDTO EditNoteTitle(System.Guid id, string title);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/EditNoteTitle", ReplyAction="http://tempuri.org/INotesService/EditNoteTitleResponse")]
        System.Threading.Tasks.Task<Notes.DTO.NoteDTO> EditNoteTitleAsync(System.Guid id, string title);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/EditNoteText", ReplyAction="http://tempuri.org/INotesService/EditNoteTextResponse")]
        Notes.DTO.NoteDTO EditNoteText(System.Guid id, string text);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/EditNoteText", ReplyAction="http://tempuri.org/INotesService/EditNoteTextResponse")]
        System.Threading.Tasks.Task<Notes.DTO.NoteDTO> EditNoteTextAsync(System.Guid id, string text);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/GetNote", ReplyAction="http://tempuri.org/INotesService/GetNoteResponse")]
        Notes.DTO.NoteDTO GetNote(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotesService/GetNote", ReplyAction="http://tempuri.org/INotesService/GetNoteResponse")]
        System.Threading.Tasks.Task<Notes.DTO.NoteDTO> GetNoteAsync(System.Guid id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INotesServiceChannel : Notes.Tester.NotesServiceIIS.INotesService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NotesServiceClient : System.ServiceModel.ClientBase<Notes.Tester.NotesServiceIIS.INotesService>, Notes.Tester.NotesServiceIIS.INotesService {
        
        public NotesServiceClient() {
        }
        
        public NotesServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NotesServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NotesServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NotesServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Notes.DTO.ServiceCustomerDTO CustomerRegistration(Notes.DTO.ClientCustomerDTO info) {
            return base.Channel.CustomerRegistration(info);
        }
        
        public System.Threading.Tasks.Task<Notes.DTO.ServiceCustomerDTO> CustomerRegistrationAsync(Notes.DTO.ClientCustomerDTO info) {
            return base.Channel.CustomerRegistrationAsync(info);
        }
        
        public Notes.DTO.ServiceCustomerDTO UpdateCustomer(System.Guid guid, Notes.DTO.ClientCustomerDTO customer) {
            return base.Channel.UpdateCustomer(guid, customer);
        }
        
        public System.Threading.Tasks.Task<Notes.DTO.ServiceCustomerDTO> UpdateCustomerAsync(System.Guid guid, Notes.DTO.ClientCustomerDTO customer) {
            return base.Channel.UpdateCustomerAsync(guid, customer);
        }
        
        public void DeleteCustomer(System.Guid guid) {
            base.Channel.DeleteCustomer(guid);
        }
        
        public System.Threading.Tasks.Task DeleteCustomerAsync(System.Guid guid) {
            return base.Channel.DeleteCustomerAsync(guid);
        }
        
        public Notes.DTO.ServiceCustomerDTO Login(Notes.DTO.AuthorizationDTO authorizationInformation) {
            return base.Channel.Login(authorizationInformation);
        }
        
        public System.Threading.Tasks.Task<Notes.DTO.ServiceCustomerDTO> LoginAsync(Notes.DTO.AuthorizationDTO authorizationInformation) {
            return base.Channel.LoginAsync(authorizationInformation);
        }
        
        public Notes.DTO.ShortNoteDTO[] GetCustomersShortNotes(string login) {
            return base.Channel.GetCustomersShortNotes(login);
        }
        
        public System.Threading.Tasks.Task<Notes.DTO.ShortNoteDTO[]> GetCustomersShortNotesAsync(string login) {
            return base.Channel.GetCustomersShortNotesAsync(login);
        }
        
        public Notes.DTO.NoteDTO AddNote(string login, string title, string text) {
            return base.Channel.AddNote(login, title, text);
        }
        
        public System.Threading.Tasks.Task<Notes.DTO.NoteDTO> AddNoteAsync(string login, string title, string text) {
            return base.Channel.AddNoteAsync(login, title, text);
        }
        
        public Notes.DTO.NoteDTO EditNote(System.Guid id, string title, string text) {
            return base.Channel.EditNote(id, title, text);
        }
        
        public System.Threading.Tasks.Task<Notes.DTO.NoteDTO> EditNoteAsync(System.Guid id, string title, string text) {
            return base.Channel.EditNoteAsync(id, title, text);
        }
        
        public Notes.DTO.NoteDTO EditNoteTitle(System.Guid id, string title) {
            return base.Channel.EditNoteTitle(id, title);
        }
        
        public System.Threading.Tasks.Task<Notes.DTO.NoteDTO> EditNoteTitleAsync(System.Guid id, string title) {
            return base.Channel.EditNoteTitleAsync(id, title);
        }
        
        public Notes.DTO.NoteDTO EditNoteText(System.Guid id, string text) {
            return base.Channel.EditNoteText(id, text);
        }
        
        public System.Threading.Tasks.Task<Notes.DTO.NoteDTO> EditNoteTextAsync(System.Guid id, string text) {
            return base.Channel.EditNoteTextAsync(id, text);
        }
        
        public Notes.DTO.NoteDTO GetNote(System.Guid id) {
            return base.Channel.GetNote(id);
        }
        
        public System.Threading.Tasks.Task<Notes.DTO.NoteDTO> GetNoteAsync(System.Guid id) {
            return base.Channel.GetNoteAsync(id);
        }
    }
}
