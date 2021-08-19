using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace GenericServices
{
    public class HttpClientService
    {
        //Favouring the creation of a single static instance of an HTTPClient over multiple instances wrapped in a using statement to avoid socket creation and deletion issues.
        static readonly HttpClient client = new HttpClient();
        
        
        public static async Task<HttpResponseMessage> SendRequest(Uri uri)
        {
            HttpResponseMessage response;

            try
            {
                ValidateUri(uri);

                response = await client.GetAsync(uri);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);

                throw;
            }
            catch(Exception e)
            {
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

//return "[{\"$type\":\"Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities\",\"id\":\"a24\",\"displayName\":\"A24\",\"statusSeverity\":\"Good\",\"statusSeverityDescription\":\"No Exceptional Delays\",\"bounds\":\"[[-0.23393, 51.33958],[-0.10287,51.49159]]\",\"envelope\":\"[[-0.23393, 51.33958],[-0.23393,51.49159],[-0.10287,51.49159],[-0.10287,51.33958],[-0.23393,51.33958]]\",\"url\":\" / Road / a24\"}]";

