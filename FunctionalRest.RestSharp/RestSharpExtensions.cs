using RestSharp;
using System;

namespace FunctionalRest.RestSharp
{
    public static class RestSharpExtensions
    {
        public static Func<IRestClient, IRestRequest, RestSharpClientRequestHandler> Factory = (client, request) =>
            new RestSharpClientRequestHandler()
            {
                Client = client,
                Request = request,
            };

        public static IClientRequestHandler SendsRequest(this IRestClient client, string url, Method method)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            var request = new RestRequest(url, method);
            return Factory(client, request);
        }
    }
}
