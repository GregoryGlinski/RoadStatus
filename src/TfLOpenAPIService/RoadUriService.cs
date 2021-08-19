using System;

namespace TfLOpenApiService
{
    //Single responsibility to return a correctly formatted URI which requests the Road for the provided id 
    public class RoadUriService : UriService
    {
        private string api = "Road";
        public override Uri GetUri(string id)
        {
            //Obtain base uri
            UriBuilder uriBuilder = GetBaseUriBuilder();
           
            //Add path and query specific to a road request
            uriBuilder.Path = api + '/' + id;
            uriBuilder.Query = "app_key=" + app_key;

            return uriBuilder.Uri;
        }
    }
}
