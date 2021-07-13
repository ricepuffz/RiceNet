using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiceNet;

namespace RiceNetTests
{
    [TestClass]
    public class ConnectionTest
    {
        [TestMethod]
        public void TestTest()
        {
            string expected = "123";
            string result = Connection.Test();

            Assert.AreEqual(expected, result);
        }
    }
}
