

using ImageService.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class CloseCommand : ICommand
    {

        private ImageServer server;

        public CloseCommand(ImageServer imgServer)
        {
            this.server = imgServer;
        }
        public string Execute(string[] args, out bool result)
        {
            
            result = false;
            string filepath = args[0];
            this.server.CloseHandler(filepath);
            return filepath + "closed\n";
        }
    }
}
