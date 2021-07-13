using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiceNet;

namespace RiceNetTests
{
    [TestClass]
    public class ClientTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Thread serverThread = new Thread(new ThreadStart(delegate {
                new Server(25347);
            }));
            serverThread.Start();

            Client client = new Client("localhost", 25347);
            TcpClient resultingClient = (TcpClient)typeof(Client).GetField("client", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(client);
            Assert.IsNotNull(resultingClient);
        }
    }
}
