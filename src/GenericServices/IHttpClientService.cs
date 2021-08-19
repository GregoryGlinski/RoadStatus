using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;


namespace GenericServices
{
    public interface IHttpClientService
    {
         Task<HttpResponseMessage> SendRequest(Uri uri);
    }
}
