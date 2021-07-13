using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace RiceNet
{
    public class Client
    {
        private readonly TcpClient client;
        private readonly NetworkStream networkStream;

        public Client(string hostname, int port)
        {
            client = new TcpClient(hostname, port);
            networkStream = client.GetStream();
        }

        public Client(TcpClient client)
        {
            this.client = client;
            networkStream = client.GetStream();
        }

        public void SendString(string toSend)
        {
            byte[] bytesToSend = Encoding.ASCII.GetBytes(toSend);
            networkStream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        public string ReceiveString()
        {
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = networkStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            return Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
        }

        public void Close()
        {
            client.Close();
        }
    }
}
