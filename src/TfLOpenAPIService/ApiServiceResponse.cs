using System;

namespace TfLOpenApiService
{
    public class ApiServiceResponse<T> : IApiServiceResponse<T>
    {
        bool _immutable = false;
        ResponseStatusCode _responseStatusCode = ResponseStatusCode.Unknown;
        Exception _exception;
        T _instance;

        public bool Initialized { get { return _immutable; } }
        public ResponseStatusCode ResponseStatusCode
        {
            get { return _responseStatusCode; }
            set
            {
                CheckIfImmutable();
                _responseStatusCode = value;
            }
        }
        public Exception Exception
        {
            get { return _exception; }
            set
            {
                CheckIfImmutable();
                _exception = value;
            }
        }
        public T Instance
        {
            get {return _instance;}
            set
            {
                CheckIfImmutable();
                _instance = value;
            }
        }

        public void MakeImmutable()
        {
            _immutable = true;
        }

        private void CheckIfImmutable()
        {
            if (_immutable)
                throw new System.InvalidOperationException("Invalid attempt to initialize immutable ApiServiceResponse<T> property.");
        }

        public void Initialize(ResponseStatusCode responseStatusCode, Exception exception, T instance)
        {
            CheckIfImmutable();

            _responseStatusCode = responseStatusCode;
            _exception = exception;
            _instance = instance;

            MakeImmutable();
        }
    }
}
