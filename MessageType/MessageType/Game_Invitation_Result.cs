using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageType
{
    [Serializable]
    public class Game_Invitation_Result
    {
        public bool Result { get; set; }
        public string Error { get; set; }
        public Game_Invitation_Result(bool result, string error)
        {
            this.Result = result;
            this.Error = error;
        }
    }
}
