using GenericServices;

namespace TfLOpenApiService
{
    /// <summary>
    /// Specific implementation of the generic ApiService<T> class
    /// </summary>
    public class RoadApiService : ApiService<Road>
    {
        public RoadApiService(IUriService uriService, IHttpClientService httpClientServiceProvider)
        {
            base.uriService = uriService;
            base.httpClientServiceProvider = httpClientServiceProvider;
        }
    }
}
