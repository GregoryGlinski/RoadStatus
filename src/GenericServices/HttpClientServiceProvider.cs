using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;


namespace GenericServices
{
    public class HttpClientServiceProvider : IHttpClientService
    {
        public async Task<HttpResponseMessage> SendRequest(Uri uri)
        {
            HttpResponseMessage response = await HttpClientService.SendRequest(uri);

            return response;
        }
    }
}
