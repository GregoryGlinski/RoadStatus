using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using GenericServices;

using System.Text.Json;


namespace TfLOpenApiService
{
    //HttpResponseMessage cannot be returned as it should be disposed. Mirroring requred status codes.
    public enum ResponseStatusCode { OK, NotFound, Other, Unknown};

    //Generic class. Advantage: Follows DRY principles. Disadvantage: Constructor injection not possible, therefore property injection required.
    public class ApiService<T> : IApiService<T>
    {
        //Property injection used as generic class prohibits constructor injection. This is set at creation in the factory.  
        public IUriService uriService { get; init; }
        public IHttpClientService httpClientServiceProvider { get; init; }


        public async Task<ApiServiceResponse<T>> GetApiServiceResponse(string id)
        {
            //Create instance of ApiServiceResponse<T> to return the processed response (or failure details).
            //HttpResponseMessage itself cannot be returned as it should be disposed.
            ApiServiceResponse<T> apiServiceResponse = new ApiServiceResponse<T>();
       
            //Send request to API server and receive response
            HttpResponseMessage response = await httpClientServiceProvider.SendRequest(uriService.GetUri(id));

            try
            {
                //Check response status and prepare input vaues to ApiServiceResponse<T>
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        apiServiceResponse.ResponseStatusCode = ResponseStatusCode.OK;
                        string responseBody = await response.Content.ReadAsStringAsync();
                        List<T> instanceList = JsonSerializer.Deserialize<List<T>>(responseBody);
                        apiServiceResponse.Instance = instanceList[0];
                        break;

                    case HttpStatusCode.NotFound:
                        apiServiceResponse.ResponseStatusCode = ResponseStatusCode.NotFound;
                        break;

                    //Far more specific status codes other than OK and NotFound could be handled here if necessary.

                    default:
                        apiServiceResponse.ResponseStatusCode = ResponseStatusCode.Other;
                        break;
                }
            }
            catch(Exception e)
            {
                //Does not alter apiServiceResponse.ResponseStatusCode leave in case an exception occurs after any HTTPStatusCode
                apiServiceResponse.Exception = e;
            }
            finally
            {
                //Under certain conditions a HttpResponseMessage can cuase unwanted behaviour unless unreleased resources e.g. Sockets are disposed.
                response.Dispose();
            }

            apiServiceResponse.MakeImmutable();

            return apiServiceResponse;
        }
    }
}
