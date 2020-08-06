using System;
namespace MessageType
{
    [Serializable]
    public class User
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }

        public User(string account, string password, string nickName)
        {
            this.Account = account;
            this.Password = password;
            this.NickName = nickName;
        }
    }
}
