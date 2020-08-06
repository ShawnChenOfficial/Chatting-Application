using System;
using System.IO;
using System.Net.Sockets;
namespace MessageType
{
    [Serializable]
    public class Message
    {
        public object Data { get; set; }
        public string AccountFrom { get; set; }
        public string AccountTo { get; set; }

        public Message(object data, string accountFrom, string accountTo)
        {
            this.Data = data;
            this.AccountFrom = accountFrom;
            this.AccountTo = accountTo;
        }
    }
}
