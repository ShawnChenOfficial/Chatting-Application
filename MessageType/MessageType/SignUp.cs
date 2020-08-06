using System;
namespace MessageType
{
    [Serializable]
    public class SignUp
    {
        public string NewAccount { get; set; }
        public string NewPassword { get; set; }

        public SignUp(string newAccount, string newPassword)
        {
            this.NewAccount = newAccount;
            this.NewPassword = newPassword;
        }
    }
}
