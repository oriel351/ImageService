using Newtonsoft.Json;
using SharedLib;
using SharedLib.Event;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Server
{
    class TcpServer
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        private List<TcpClient> clients;
        Mutex mux;
        

        public TcpServer(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
            this.clients = new List<TcpClient>();
            this.mux = new Mutex();         
        }

        public void start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);

            listener.Start();
            Console.WriteLine("Waiting for connections...");

            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        this.clients.Add(client);
                        Console.WriteLine("Got new connection");                       
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }


        public void NotifyAllClients(Object sender, CommandRecievedEventArgs e)
        {
            foreach (TcpClient client in this.clients)
            {
                this.ch.SendData(client, JsonConvert.SerializeObject(e));          
            }
        }


        public void NotifyAllClients(Object sender, MessageRecievedEventArgs e)
        {
            foreach (TcpClient client in this.clients)
            {
                this.ch.SendData(client, JsonConvert.SerializeObject(e));
            }
        }


    }
}