



using ImageService.Commands;
using SharedLib.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using ImageService.Server;
using System.Collections.Generic;
using SharedLib;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        private IImageServiceModal m_modal;           // The Model Object
        //private ILoggingService m_logging;
        //private ImageServer imgServer;
        private Dictionary<int, ICommand> commands;

        public ImageController(IImageServiceModal modal, ILoggingService m_logging,
            ImageServer imgServer, ConfigData config)
        {
            m_modal = modal;  // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>()
            {
                { (int)CommandEnum.NewFileCommand, new NewFileCommand(this.m_modal) },
                { (int)CommandEnum.CloseCommand, new CloseCommand(imgServer) },
                { (int)CommandEnum.LogCommand, new LogCommand(m_logging)},
                { (int)CommandEnum.GetConfigCommand, new ConfigCommand(config)}
            };
        }
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {           
            resultSuccesful = false;
            ICommand a = this.commands[commandID];
            return a.Execute(args, out resultSuccesful);       
        }
    }
}
