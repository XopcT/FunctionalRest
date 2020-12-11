using RestSharp;
using System;
using System.Net;

namespace FunctionalRest.RestSharp
{
    public class RestSharpResponse<TData> : IFunctionalResponse<TData>
    {
        public RestSharpResponse(IRestResponse<TData> original)
        {
            this.original = original ?? throw new ArgumentNullException(nameof(original));
        }

        public HttpStatusCode GetCode() => this.original.StatusCode;

        public TData GetData() => this.original.Data;

        public byte[] GetFile() => this.original.RawBytes;

        public bool GetSuccess() => this.original.IsSuccessful;

        private readonly IRestResponse<TData> original = null;
    }
}
