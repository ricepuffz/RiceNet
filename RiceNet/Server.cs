using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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

            new Thread(new ThreadStart(ListenLoop)).Start();
        }

        public int AcceptedClientsCount()
        {
            return acceptedClients.Count;
        }

        public AcceptedClient GetAcceptedClient(int ID)
        {
            return acceptedClients[ID];
        }

        public void Stop()
        {
            running = false;
            listener.Stop();

            foreach (AcceptedClient client in acceptedClients)
                client.Client.Close();
        }

        private void ListenLoop()
        {
            listener.Start();

            while (running)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    acceptedClients.Add(new AcceptedClient(client, NextClientID));
                }
                catch (SocketException) when (!running)
                {
                    if (running == false)
                        Console.WriteLine("SocketException thrown due to Server stop, this should be totally fine");
                }
            }
        }
    }
}
