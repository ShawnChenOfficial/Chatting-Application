using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageType
{
    [Serializable]
    public class Chats_Info
    {
        public string Chater { get; set; }
        public List<string> ChatsInfor { get; set; }

        public Chats_Info(string chater, List<string>chatsInfor)
        {
            this.Chater = chater;
            this.ChatsInfor = chatsInfor;
        }
    }
}
