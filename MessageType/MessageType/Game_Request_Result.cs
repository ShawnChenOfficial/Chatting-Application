using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageType
{
    [Serializable]
    public class Game_Request_Result
    {
        public string Inviter { get; set; }
        public string Accepter { get; set; }
        public bool Begin { get; set; }
        public string First { get; set; }
        public string Second { get; set; }
        public Game_Request_Result(string inviter, string accepter, bool begin,string first, string second)
        {
            this.Inviter = inviter;
            this.Accepter = accepter;
            this.Begin = begin;
            this.First = first;
            this.Second = second;
        }
    }
}
