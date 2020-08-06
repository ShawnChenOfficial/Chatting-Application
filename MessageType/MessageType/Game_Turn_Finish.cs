using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageType
{
    [Serializable]
    public class Game_Turn_Finish
    {
        public string User { get; set; }
        public string Opponent { get; set; }
        public string Movement { get; set; }
        public List<string> Steps { get; set; }
        public Game_Turn_Finish(string user,string opponent, string movement, List<string> steps)
        {
            this.User = user;
            this.Opponent = opponent;
            this.Movement = movement;
            this.Steps = steps;
        }
    }
}
