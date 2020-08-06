using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageType
{
    [Serializable]
    public class Game_Quit
    {
        public string You { get; set; }
        public string Opponent { get; set; }

        public Game_Quit(string you, string opponent)
        {
            this.You = you;
            this.Opponent = opponent;
        }
    }
}
