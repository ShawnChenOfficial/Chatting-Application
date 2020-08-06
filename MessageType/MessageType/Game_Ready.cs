using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageType
{
    [Serializable]
    public class Game_Ready
    {
        public string User { get; set; }
        public bool Ready { get; set; }
        public Game_Ready(string user, bool ready)
        {
            this.User = user;
            this.Ready = ready;
        }
    }
}
