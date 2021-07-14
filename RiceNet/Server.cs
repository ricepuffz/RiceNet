using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using RiceLog;

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
            Logger.Log($"Starting server on port {port}...");

            IPAddress localAddress = IPAddress.Parse(SERVER_IP);
            listener = new TcpListener(localAddress, port);

            Logger.Log("Server successfully started");

            new Thread(new ThreadStart(ListenLoop)).Start();

            Logger.Log($"Server is now listening on port {port}");
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
            Logger.Log("Server stopping...");

            running = false;
            listener.Stop();

            Logger.Log("Closing client connections...");

            foreach (AcceptedClient client in acceptedClients)
                client.Client.Close();

            Logger.Log("Client connections closed");

            Logger.Log("Server stopped");
        }

        private void ListenLoop()
        {
            listener.Start();

            while (running)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Logger.Log($"Accepted new client connection with ID {NextClientID}");
                    acceptedClients.Add(new AcceptedClient(client, NextClientID));
                }
                catch (SocketException) when (!running)
                {
                    if (running == false)
                        Logger.Log("SocketException thrown due to Server stop, this should be totally fine", Logger.VERBOSITY.WARN);
                }
            }
        }
    }
}
