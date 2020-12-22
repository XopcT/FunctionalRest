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
        public void RestSharpResponseShouldReturnSuccessFlagFromWrappedResult()
        {
            var response = new Mock<IRestResponse<int>>();
            response.Setup(x => x.IsSuccessful).Returns(true);
            var tested = new RestSharpResponse<int>(response.Object);
            Assert.IsTrue(tested.GetSuccess());
        }

        [TestMethod()]
        public void RestSharpResponseShouldReturnStatusCodeFromWrappedResult()
        {
            var response = new Mock<IRestResponse<int>>();
            response.Setup(x => x.StatusCode).Returns(HttpStatusCode.OK);
            var tested = new RestSharpResponse<int>(response.Object);
            Assert.AreEqual(HttpStatusCode.OK, tested.GetCode());
        }

        [TestMethod()]
        public void RestSharpResponseShouldReturnDataFromWrappedResult()
        {
            var response = new Mock<IRestResponse<int>>();
            response.Setup(x => x.Data).Returns(4);
            var tested = new RestSharpResponse<int>(response.Object);
            Assert.AreEqual(4, tested.GetData());
        }

        [TestMethod()]
        public void RestSharpResponseShouldReturnFileFromWrappedResult()
        {
            var fileData = new byte[] { 1, 2, 3 };
            var response = new Mock<IRestResponse<int>>();
            response.Setup(x => x.RawBytes).Returns(fileData);
            var tested = new RestSharpResponse<int>(response.Object);
            Assert.AreEqual(fileData, tested.GetFile());
        }

        [TestMethod()]
        public void RestSharpResponseShouldReturnExceptionFromWrappedResult()
        {
            var ex = new System.InvalidOperationException();
            var response = new Mock<IRestResponse<int>>();
            response.Setup(x => x.ErrorException).Returns(ex);
            var tested = new RestSharpResponse<int>(response.Object);
            Assert.AreEqual(ex, tested.GetException());
        }
    }
}
