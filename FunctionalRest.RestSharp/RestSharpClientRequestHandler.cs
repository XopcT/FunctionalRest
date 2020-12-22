using RestSharp;
using RestSharp.Authenticators;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionalRest.RestSharp
{
    public class RestSharpClientRequestHandler : IClientRequestHandler
    {
        public virtual void AddJwtAuthentication(string accessToken)
        {
            this.Client.Authenticator = new JwtAuthenticator(accessToken);
        }

        public void AddHeader(string name, string value)
        {
            this.Request.AddHeader(name, value);
        }

        public virtual void AddJsonBody<T>(T body)
        {
            this.Request.AddJsonBody(body);
        }

        public virtual void AddFile(string name, string fileName, string contentType, long contentLength, Action<Stream> data)
        {
            this.Request.AddFile(name, data, fileName, contentLength, contentType);
        }

        public virtual async Task<IFunctionalResponse<TData>> GetResponseAsync<TData>(CancellationToken cancellationToken)
        {
            var response = await this.Client.ExecuteAsync<TData>(this.Request, cancellationToken);
            return new RestSharpResponse<TData>(response);
        }

        public virtual Task<byte[]> DownloadAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IRestClient Client { get; set; }
        public IRestRequest Request { get; set; }
    }
}
