using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace RiceNet
{
    public class Server
    {
        private const string SERVER_IP = "127.0.0.1";
        private readonly TcpListener listener;

        private bool running = true;
        private readonly List<AcceptedClient> acceptedClients = new List<AcceptedClient>();

        private int NextClientID
        {
            get
            {
                return acceptedClients.Count;
            }
        }

        public Server(int port)
        {
            IPAddress localAddress = IPAddress.Parse(SERVER_IP);
            listener = new TcpListener(localAddress, port);

            ListenLoop();
        }

        public AcceptedClient GetAcceptedClient(int ID)
        {
            return acceptedClients[ID];
        }

        public void Stop()
        {
            running = false;
            try
            {
                listener.Stop();
            }
            catch (SocketException)
            {
                Console.WriteLine("SocketException thrown due to Server stop, this should be totally fine");
            }
        }

        private void ListenLoop()
        {
            listener.Start();

            while (running)
            {
                TcpClient client = listener.AcceptTcpClient();
                acceptedClients.Add(new AcceptedClient(client, NextClientID));
            }

            foreach (Client client in acceptedClients)
                client.Close();
        }
    }
}
