using System.Threading.Tasks;

namespace TfLOpenApiService
{
    public interface IApiService<T>
    {
        public Task<ApiServiceResponse<T>> GetApiServiceResponse(string id);
    }
}
