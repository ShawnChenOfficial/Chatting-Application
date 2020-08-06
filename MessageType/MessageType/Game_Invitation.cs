using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageType
{
    [Serializable]
    public class Game_Invitation
    {
        public string Inviter { get; set; }
        public string Invitation_Reveiver { get; set; }

        public Game_Invitation(string inviter, string invitation_Reveiver)
        {
            this.Invitation_Reveiver = invitation_Reveiver;
            this.Inviter = inviter;
        }
    }
}
