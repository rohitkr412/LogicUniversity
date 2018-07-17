using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LogicUniversity.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/EchoWithGet/{s}", ResponseFormat = WebMessageFormat.Json)]
        string EchoWithGet(string s);

        [OperationContract]
        [WebInvoke]
        string EchoWithPost(string s);
    }
}
