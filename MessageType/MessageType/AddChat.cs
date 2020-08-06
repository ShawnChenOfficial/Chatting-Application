using System;
namespace MessageType
{
    [Serializable]
    public class AddChat
    {
        public string ThisAccount { get; set; }
        public string TargetAccount { get; set; }
        public AddChat(string thisAccount, string targetAccount)
        {
            this.ThisAccount = thisAccount;
            this.TargetAccount = targetAccount;
        }
    }
}
