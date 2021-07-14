using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace RiceNet
{
    public class AcceptedClient
    {
        public int ID { get; private set; }
        public Client Client { get; private set; }

        public AcceptedClient(TcpClient client, int id)
        {
            ID = id;
            Client = new Client(client);
        }
    }
}
