using System;

namespace SharedLib
{
    public class MessageRecievedEventArgs : EventArgs
    {
        //every time this class gets a message it should
        public Infrastructure.Enums.MessageTypeEnum Status { get; set; }
        public string Message { get; set; }
        public MessageRecievedEventArgs(Infrastructure.Enums.MessageTypeEnum status, string message)
        {
            //דקה
            //אני בודקת דברים אצלי במחשב
            this.Status = status;
            this.Message = message;
        }
    }
}