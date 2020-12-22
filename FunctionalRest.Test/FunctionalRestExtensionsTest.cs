using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionalRest.Test
{
    [TestClass()]
    public class FunctionalRestExtensionsTest
    {
        [TestMethod()]
        public void WithJwtAuthenticationExtensionMethodShouldCallAddJwtAuthentication()
        {
            var handler = new Mock<IClientRequestHandler>();
            handler.Object.WithJwtAuthentication("access_token");
            handler.Verify(x => x.AddJwtAuthentication("access_token"), Times.Once());
        }

        [TestMethod()]
        public void WithHeaderExtensionMethodSouldCallAddHeader()
        {
            var handler = new Mock<IClientRequestHandler>();
            handler.Object.WithHeader("name", "value");
            handler.Verify(x => x.AddHeader("name", "value"), Times.Once());
        }

        [TestMethod()]
        public void WithJsonBodyExtensionMethodShouldCallAddJsonBody()
        {
            var body = new { foo = 123, bar = 456 };
            var handler = new Mock<IClientRequestHandler>();
            handler.Object.WithJsonBody(body);
            handler.Verify(x => x.AddJsonBody(body), Times.Once());
        }

        [TestMethod()]
        public void WithFileExtensionMethodShouldCallAddFile()
        {
            var handler = new Mock<IClientRequestHandler>();
            handler.Object.WithFile("aaa", "bbb", "image/jpeg", 123, data => { });
            handler.Verify(x => x.AddFile("aaa", "bbb", "image/jpeg", 123, It.IsAny<Action<Stream>>()), Times.Once());
        }

        [TestMethod()]
        public async Task ExecuteAsyncShouldCallOnSuccessMethodWhenResultIsOk()
        {
            var response = new Mock<IFunctionalResponse<int>>();
            response.Setup(x => x.GetSuccess()).Returns(true);
            response.Setup(x => x.GetData()).Returns(4);
            var handler = new Mock<IClientRequestHandler>();
            handler.Setup(x => x.GetResponseAsync<int>(It.IsAny<CancellationToken>())).ReturnsAsync(response.Object);

            var onSuccessCalled = false;
            var onErrorCalled = false;
            var result = await handler.Object.ExecuteAsync<int, int>(onSuccess: x => { onSuccessCalled = true; return x.GetData(); },
                onError: x => { onErrorCalled = true; }, CancellationToken.None);
            Assert.AreEqual(4, result);
            Assert.IsTrue(onSuccessCalled);
            Assert.IsFalse(onErrorCalled);
        }

        [TestMethod()]
        public async Task ExecuteAsyncShouldCallOnErrorMethodWhenResultContainsErxception()
        {
            var response = new Mock<IFunctionalResponse<int>>();
            response.Setup(x => x.GetSuccess()).Returns(false);
            var handler = new Mock<IClientRequestHandler>();
            handler.Setup(x => x.GetResponseAsync<int>(It.IsAny<CancellationToken>())).ReturnsAsync(response.Object);

            var onSuccessCalled = false;
            var onErrorCalled = false;
            await handler.Object.ExecuteAsync<int, int>(onSuccess: x => { onSuccessCalled = true; return x.GetData(); },
                onError: x => { onErrorCalled = true; }, CancellationToken.None);
            Assert.IsFalse(onSuccessCalled);
            Assert.IsTrue(onErrorCalled);
        }
    }
}
