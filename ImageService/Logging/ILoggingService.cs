using SharedLib;
using SharedLib.Infrastructure.Enums;
using System;

namespace ImageService.Logging
{
    public interface ILoggingService
    {
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;    
        void Log(string message, MessageTypeEnum type);   // Logging the Message
      
        
    }
}
