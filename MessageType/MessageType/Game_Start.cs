using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageType
{
    [Serializable]
    public class Game_Start
    {
        public bool Start { get; set; }
        public Game_Start(bool start)
        {
            this.Start = start;
        }
    }
}
