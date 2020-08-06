using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageType
{
    [Serializable]
    public class GetWinner
    {
        public string Winner { get; set; }

        public GetWinner(string winner)
        {
            this.Winner = winner;
        }
    }
}
