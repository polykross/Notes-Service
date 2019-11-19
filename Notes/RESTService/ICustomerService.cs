using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Notes.DBModels;

namespace Notes.RESTService
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        [WebGet(UriTemplate = "Customers", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        List<Customer> GetAllCustomers();
    }
}
