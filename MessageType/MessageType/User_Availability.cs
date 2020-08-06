using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageType
{
    [Serializable]
    public class User_Availability
    {
        public string User { get; set; }
        public User_Status user_Status { get; set; }

        public User_Availability(string userAccount, User_Status status)
        {
            this.User = userAccount;
            this.user_Status = status;
        }
    }
}
