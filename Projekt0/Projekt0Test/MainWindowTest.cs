using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt0;

namespace Projekt0Test
{
    [TestClass]
    public class MainWindowTest
    {
        [TestMethod]
        public void SayHelloTest()
        {
            Assert.AreEqual(HelloFactory.SayHello(), "Hello World!");
        }
    }
}