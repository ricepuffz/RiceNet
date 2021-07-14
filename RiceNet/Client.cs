using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using RiceLog;

namespace RiceNet
{
    public class Client
    {
        private readonly TcpClient client;
        private readonly NetworkStream networkStream;

        public Client(string hostname, int port)
        {
            Logger.Log($"Client connecting to {hostname}:{port} ...");

            client = new TcpClient(hostname, port);
            networkStream = client.GetStream();

            Logger.Log("Connection established");
        }

        public Client(TcpClient client)
        {
            this.client = client;
            networkStream = client.GetStream();
        }

        public void SendString(string toSend)
        {
            Logger.Log($"Client sending '{toSend}'...");

            byte[] bytesToSend = Encoding.ASCII.GetBytes(toSend);
            networkStream.Write(bytesToSend, 0, bytesToSend.Length);

            Logger.Log($"Client sent '{toSend}'");
        }

        public string ReceiveString()
        {
            Logger.Log("Client reading incoming string...");

            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = networkStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            string result = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);

            Logger.Log($"Client read incoming message '{result}'");

            return result;
        }

        public void Close()
        {
            Logger.Log("Client closing connection...");
            client.Close();
            Logger.Log("Client closed connection");
        }
    }
}
