using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionalRest
{
    public static class RestClientExtensions
    {
        public static IClientRequestHandler WithJwtAuthentication(this IClientRequestHandler h, string token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            h.AddJwtAuthentication(token);
            return h;
        }

        public static IClientRequestHandler WithHeader(this IClientRequestHandler h, string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            h.AddHeader(name, value);
            return h;
        }

        public static IClientRequestHandler WithJsonBody<TBody>(this IClientRequestHandler h, TBody body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }
            h.AddJsonBody(body);
            return h;
        }

        public static IClientRequestHandler WithFile(this IClientRequestHandler h, string name, string fileName, string contentType, long contentLength, Action<Stream> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            h.AddFile(name, fileName, contentType, contentLength, data);
            return h;
        }

        public static Task<IFunctionalResponse<TData>> ExecuteAsync<TData>(this IClientRequestHandler h)
            => h.ExecuteAsync<TData>(CancellationToken.None);

        public static Task<IFunctionalResponse<object>> ExecuteAsync(this IClientRequestHandler h, CancellationToken cancellationToken)
            => h.ExecuteAsync<object>(cancellationToken);

        public static Task<IFunctionalResponse<object>> ExecuteAsync(this IClientRequestHandler h)
            => h.ExecuteAsync<object>(CancellationToken.None);

    }
}