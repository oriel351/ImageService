

using Newtonsoft.Json;
using SharedLib;

namespace ImageService.Commands
{
    class ConfigCommand : ICommand
    {
        private ConfigData config;
        
        public ConfigCommand(ConfigData config)
        {
            this.config = config;
        }
        public string Execute(string[] args, out bool result)
        {
            result = true;
            string data = JsonConvert.SerializeObject(this.config);
            return data;
        }
    }
}
