using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace GenericServices
{
    public class HttpClientService
    {
        //Favouring the creation of a single static instance of an HTTPClient over multiple instances wrapped in a using statement to avoid socket creation and deletion issues.
        private static readonly HttpClient client = new HttpClient();

        public static async Task<HttpResponseMessage> SendRequest(Uri uri)
        {
            HttpResponseMessage response;

            try
            {
                ValidateUri(uri);

                response = await client.GetAsync(uri);
            }
            catch
            {
                //Rethrow to caller
                throw;
            }

            return response;
        }

        //Candidate for possible refactoring to a separate helper method.
        public static void ValidateUri(Uri uri)
        {
            if(uri == null)
                throw new ArgumentNullException("The URI is missing (is null). Request to API was aborted.");


            if (uri.AbsoluteUri.Contains("http") == false || uri.AbsoluteUri.Contains("http") == false)
                throw new ArgumentException("An invalid URI was detected. Request to API was aborted.");
        }
    }
}
