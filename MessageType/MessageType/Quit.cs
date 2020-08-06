using System;
namespace MessageType
{
    [Serializable]
    public class Quit
    {
        public string Account { get; set; }
        public Quit(string account)
        {
            this.Account = account;
        }
    }
}
