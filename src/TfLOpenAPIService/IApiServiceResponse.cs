using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfLOpenApiService
{
    public interface IApiServiceResponse<T>
    {
        public bool Initialized { get; }
        public ResponseStatusCode ResponseStatusCode { get; }
        public Exception Exception { get; }
        public T Instance { get; }
    }
}
