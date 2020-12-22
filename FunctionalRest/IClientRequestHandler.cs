using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionalRest
{
    public interface IClientRequestHandler
    {
        void AddJwtAuthentication(string accessToken);

        void AddJsonBody<T>(T body);

        void AddHeader(string name, string value);

        void AddFile(string name, string fileName, string contentType, long contentLength, Action<Stream> data);

        Task<IFunctionalResponse<TData>> GetResponseAsync<TData>(CancellationToken cancellationToken);

        Task<byte[]> DownloadAsync(CancellationToken cancellationToken);
    }
}
