
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public class LogObject
    {
        private string data;
        private MessageTypeEnum mtype;
        public string EnumType
        {
            get { return Enum.GetName(typeof(MessageTypeEnum), mtype); }
            set { this.mtype = (MessageTypeEnum)Enum.Parse(typeof(MessageTypeEnum), value); }
        }

        public string Data
        {
            get { return data; }
            set { data = value; } // ani lo yahol laaanot ani beemtza siha ba  rabotatekef mesayemet
        }

    }//החלטנו שלא משתמשים בכלל בזה?
}