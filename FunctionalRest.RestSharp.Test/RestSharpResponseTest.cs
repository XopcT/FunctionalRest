using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System.Net;

namespace FunctionalRest.RestSharp.Test
{
    [TestClass()]
    public class RestSharpResponseTest
    {
        [TestMethod()]
        public void RestSharpResponseShouldReturnValuesFromWrappedRequest()
        {
            var fileData = new byte[] { 1, 2, 3 };
            var response = new Mock<IRestResponse<int>>();
            response.Setup(x => x.IsSuccessful).Returns(true);
            response.Setup(x => x.StatusCode).Returns(HttpStatusCode.OK);
            response.Setup(x => x.Data).Returns(4);
            response.Setup(x => x.RawBytes).Returns(fileData);

            var tested = new RestSharpResponse<int>(response.Object);
            Assert.IsTrue(tested.GetSuccess());
            Assert.AreEqual(HttpStatusCode.OK, tested.GetCode());
            Assert.AreEqual(4, tested.GetData());
            Assert.AreEqual(fileData, tested.GetFile());
        }
    }
}
