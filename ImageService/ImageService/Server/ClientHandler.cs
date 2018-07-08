using ImageService.Controller;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedLib.Event;
using SharedLib.Infrastructure.Enums;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Server
{
    public class ClientHandler : IClientHandler
    {
        private Mutex mux;
        private IImageController m_controller;       

        public ClientHandler(IImageController m_controller)
        {
            this.m_controller = m_controller;
            this.mux = new Mutex();
        }
        public void HandleClient(TcpClient client)
        {
            new Task(() => 
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))                
                {
                    bool result;
                    while (client.Connected)
                    {
                        string commandLine = reader.ReadToEnd();
                        CommandRecievedEventArgs cmd = GetData(commandLine);
                        int command = cmd.CommandID;

                        switch (command)
                        {
                            case (int)CommandEnum.GetConfigCommand:
                                string data = this.m_controller.ExecuteCommand(command, null, out result);
                                SendData(client, data);
                                break;
                            case (int)CommandEnum.LogCommand:                                
                                data = this.m_controller.ExecuteCommand
                                (command, null, out result);
                                SendData(client, data);
                                break;
                            case (int)CommandEnum.CloseCommand:
                                this.m_controller.ExecuteCommand(command, cmd.Args, out result);
                                
                                break;
                        }
                    }  
                   
                }
                client.Close();
            }).Start();

        }

        public void SendData(TcpClient client, string data)
        {
            using (StreamWriter writer = new StreamWriter(client.GetStream()))
            {
                mux.WaitOne();
                writer.Write(data);
                mux.ReleaseMutex();
            }
        }

        public CommandRecievedEventArgs GetData(string data)
        {
            CommandRecievedEventArgs cmd = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(data);
            JObject dt = JObject.Parse(data);
            return cmd;
        }

        
    }

}
