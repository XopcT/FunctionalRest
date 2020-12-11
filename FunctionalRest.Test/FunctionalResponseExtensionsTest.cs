using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FunctionalRest.Test
{
    [TestClass()]
    public class FunctionalResponseExtensionsTest
    {
        [TestMethod()]
        public void OnSuccessExtensionMethodShouldRunActionWhenResponseIsOk()
        {
            var response = new Mock<IFunctionalResponse<object>>();
            response.Setup(x => x.GetSuccess()).Returns(true);

            var called = false;
            response.Object.OnSuccess<object>(x => { called = true; });
            Assert.IsTrue(called);
        }

        [TestMethod()]
        public void OnSuccessExtensionMethodShouldNotRunActionWhenResponseHasError()
        {
            var response = new Mock<IFunctionalResponse<object>>();
            response.Setup(x => x.GetSuccess()).Returns(false);
            var called = false;
            response.Object.OnSuccess<object>(x => { called = true; });
            Assert.IsFalse(called);
        }

        [TestMethod()]
        public void OnErrorExtensionMethodShouldRunActionWhenResponseHasError()
        {
            var response = new Mock<IFunctionalResponse<object>>();
            response.Setup(x => x.GetSuccess()).Returns(false);

            var called = false;
            response.Object.OnError<object>(x => { called = true; });
            Assert.IsTrue(called);
        }

        [TestMethod()]
        public void OnErrorExtensionMethodShouldNotRunActionWhenResponseIsOk()
        {
            var response = new Mock<IFunctionalResponse<object>>();
            response.Setup(x => x.GetSuccess()).Returns(true);

            var called = false;
            response.Object.OnError<object>(x => { called = true; });
            Assert.IsFalse(called);
        }
    }
}
