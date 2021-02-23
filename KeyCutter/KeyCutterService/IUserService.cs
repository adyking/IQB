using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfServiceKeyCutter.Models;

namespace KeyCutterService
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/backdoor",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "GET")]
        User CreateUser();

    }
}
