using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Logging;
using SharedLib;
using SharedLib.Event;
using SharedLib.Infrastructure.Enums;
using System;


namespace ImageService.Server
{
    

    public class ImageServer
    {

        public const int PORT_NUMBER = 5555;
        #region Members

        // working members
        private IImageController m_controller;
        private ILoggingService m_logging;
        
        // info members
        private string outputDir;
        string[] paths;

        // communication members
        private TcpServer tcpServer;
        //private IClientHandler ch;

        #endregion

        #region Properties
        // The event that notifies about a new Command being recieved
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        // event which handles data sending to all clients
        //public event EventHandler<MessageRecievedEventArgs> MessageClients;

        #endregion

        /*
         * Constructor.
         */
        public ImageServer(IImageController controller, ILoggingService log, ConfigData conf)
        {
            this.m_controller = controller;
            this.m_logging = log;
            this.outputDir = conf.outputDir;
            this.paths = conf.paths;
            
            //this.MessageClients += this.tcpServer.NotifyAllClients;

            InitializeHandlers();
        }

        private void InitializeCommunication()
        {

            ClientHandler ch = new ClientHandler(this.m_controller);

            this.m_logging.MessageRecieved += this.tcpServer.NotifyAllClients;
            this.tcpServer = new TcpServer(PORT_NUMBER, ch);
           
        }

        private void InitializeHandlers()
        {
            foreach (string p in this.paths)
            {
                IDirectoryHandler handler = new DirectoryHandler(this.m_logging, this.m_controller, p);
                handler.StartHandleDirectory(p);

                handler.DirectoryClose += OnHandlerCloseCommand;                
                this.CommandRecieved += handler.OnCommandRecieved;
                
            }          
        }

        private void OnHandlerCloseCommand(object sender, DirectoryCloseEventArgs e)
        {            
            this.CommandRecieved -= ((DirectoryHandler)sender).OnCommandRecieved;            
        }


        public void StopServer()
        {
            foreach (string p in this.paths)
            {
                this.CommandRecieved?.Invoke(
                    this, 
                    new CommandRecievedEventArgs((int)CommandEnum.CloseCommand,
                    new string[] { }, p));
            }

            this.CommandRecieved -= this.tcpServer.NotifyAllClients;
            //this.MessageClients -= this.tcpServer.NotifyAllClients;

        }
               

        
       

        public void CloseHandler(string path)
        {
            this.CommandRecieved?.Invoke(
                this,
                new CommandRecievedEventArgs((int)CommandEnum.CloseCommand,
                new string[] { }, path));
        }
        
       
    }
}
