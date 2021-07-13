using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace RiceNet
{
    public class AcceptedClient : Client
    {
        public int ID { get; private set; }

        public AcceptedClient(TcpClient client, int id) : base(client)
        {
            ID = id;
        }
    }
}
