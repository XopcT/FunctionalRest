using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;

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

    }
}
