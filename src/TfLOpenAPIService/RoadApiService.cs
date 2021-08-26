using Microsoft.Extensions.Logging;

using GenericServices;


namespace TfLOpenApiService
{
    /// <summary>
    /// Specific implementation of the generic ApiService<T> class
    /// </summary>
    public class RoadApiService : ApiService<Road>, IApiService<Road>
    {
        public RoadApiService(IUriService uriService, IHttpClientService httpClientServiceProvider)
            : this(uriService, httpClientServiceProvider, null)
        {
           
        }

        public RoadApiService(IUriService uriService, IHttpClientService httpClientServiceProvider, ILogger<RoadApiService> logger)
        {
            base.logger = logger;
            base.uriService = uriService;
            base.httpClientServiceProvider = httpClientServiceProvider;
        }
    }
}
