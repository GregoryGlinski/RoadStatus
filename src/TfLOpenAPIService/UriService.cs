using System;

namespace TfLOpenApiService
{
    public abstract class UriService : IUriService
    {
        protected static readonly string scheme = "https";
        protected static readonly string host = "api.tfl.gov.uk";
        protected static readonly string app_key = "15670729b28d4ee091416441040d366f";

        public abstract Uri GetUri(string id);

        public UriBuilder GetBaseUriBuilder()
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = scheme;
            uriBuilder.Host = host;

            return uriBuilder;
        }
  
    }
}
