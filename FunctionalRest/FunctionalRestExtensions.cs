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

        public static async Task<TData> ExecuteAsync<TResponseData, TData>(this IClientRequestHandler h,
            Func<IFunctionalResponse<TResponseData>, TData> onSuccess,
            Action<IFunctionalResponse<TResponseData>> onError,
            CancellationToken cancellationToken = default)
        {
            var response = await h.GetResponseAsync<TResponseData>(cancellationToken);
            if (response.GetSuccess())
            {
                return onSuccess(response);
            }
            else
            {
                onError(response);
                return default;
            }
        }

        public static Task<TData> ExecuteAsync<TResponseData, TData>(this IClientRequestHandler h,
            Func<IFunctionalResponse<TResponseData>, TData> onSuccess,
            CancellationToken cancellationToken = default)
            => h.ExecuteAsync<TResponseData, TData>(onSuccess, x => { throw x.GetException(); }, cancellationToken);

        public static Task<TData> ExecuteAsync<TData>(this IClientRequestHandler h,
            Func<IFunctionalResponse<TData>, TData> onSuccess,
            CancellationToken cancellationToken = default)
            => h.ExecuteAsync<TData, TData>(onSuccess, x => { throw x.GetException(); }, cancellationToken);

        public static Task<TResponseData> ExecuteAsync<TResponseData>(this IClientRequestHandler h,
            Action<IFunctionalResponse<TResponseData>> onError,
            CancellationToken cancellationToken = default)
            => h.ExecuteAsync<TResponseData, TResponseData>(x => x.GetData(), onError, cancellationToken);
        
        public static Task<TData> ExecuteAsync<TData>(this IClientRequestHandler h, CancellationToken cancellationToken = default)
            => h.ExecuteAsync<TData, TData>(onSuccess: x => x.GetData(), onError: x => { throw x.GetException(); }, cancellationToken);

        public static Task ExecuteAsync(this IClientRequestHandler h,
            Func<IFunctionalResponse<object>, object> onSuccess,
            Action<IFunctionalResponse<object>> onError,
            CancellationToken cancellationToken = default)
            => h.ExecuteAsync<object, object>(onSuccess, onError, cancellationToken);

        public static Task ExecuteAsync(this IClientRequestHandler h,
            Func<IFunctionalResponse<object>, object> onSuccess, CancellationToken cancellationToken = default)
            => h.ExecuteAsync<object, object>(onSuccess, x => { throw x.GetException(); }, cancellationToken);

        public static Task ExecuteAsync(this IClientRequestHandler h, CancellationToken cancellationToken = default)
            => h.ExecuteAsync<object, object>(x => null, x => { throw x.GetException(); }, cancellationToken);

    }
}