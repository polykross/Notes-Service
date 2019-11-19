using Notes.DBModels;
using Notes.DTO;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Notes.RESTService.Controller
{
    [ServiceContract]
    public interface ICustomerController
    {
        [OperationContract]
        [WebGet(UriTemplate = "Customers", 
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json)]
        List<CustomerDTO> GetAllCustomers();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Customer",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        Customer GetCustomer(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddCustomer",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        OperationResult AddCustomer(CustomerDTO customerDto);
    }
}
