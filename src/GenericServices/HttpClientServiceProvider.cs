using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using System.Net.Http;


namespace GenericServices
{
    public class HttpClientServiceProvider : IHttpClientService
    {
        private ILogger<HttpClientServiceProvider> _logger;

        public HttpClientServiceProvider()
        {
            //_logger is null
        }

        public HttpClientServiceProvider(ILogger<HttpClientServiceProvider> logger)
        {
            _logger = logger;
        }

        public async Task<HttpResponseMessage> SendRequest(Uri uri)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await HttpClientService.SendRequest(uri);
            }
            catch(SystemException e)
            {
                _logger.LogError(e.Message);

                //Should really add specific exception type handling but in all cases this should rethrow the exception to be handled by the caller so effectively the same.
                //No point trying to return the response
                throw;
            }

            return response;
        }
    }
}
