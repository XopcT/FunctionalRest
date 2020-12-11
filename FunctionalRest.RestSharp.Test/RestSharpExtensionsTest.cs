using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace FunctionalRest.RestSharp.Test
{
    [TestClass()]
    public class RestSharpExtensionsTest
    {
        class CustomRestSharpClientRequestHandler : RestSharpClientRequestHandler
        {
        }

        [TestMethod()]
        public void SendsRequestShouldCreateRestSharpClientRequestHandlersUsingFactory()
        {
            RestSharpExtensions.Factory = (c, r) => new CustomRestSharpClientRequestHandler()
            {
                Client = c,
                Request = r,
            };
            var client = new Mock<IRestClient>();
            var result = client.Object.SendsRequest("https://google.com", Method.GET) as CustomRestSharpClientRequestHandler;
            Assert.IsNotNull(result);
            Assert.AreEqual(client.Object, result.Client);
            Assert.IsNotNull(result.Request);
            Assert.AreEqual("https://google.com", result.Request.Resource);
            Assert.AreEqual(Method.GET, result.Request.Method);
        }

    }
}
