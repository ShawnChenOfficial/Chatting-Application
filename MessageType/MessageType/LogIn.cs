using System;
namespace MessageType
{
    [Serializable]
    public class LogIn
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public LogIn(string account, string password)
        {
            this.Account = account;
            this.Password = password;
        }
        public override bool Equals(object obj)
        {
            LogIn login = obj as LogIn;

            return (Account.Equals(login.Account) && Password.Equals(login.Password));
        }
        public override int GetHashCode()
        {
            return int.Parse(Account) * 7 + 1;
        }

        public static bool operator ==(LogIn login1, LogIn LogIn2)
        {
            return (login1.Account == LogIn2.Account && login1.Password == LogIn2.Password);
        }
        public static bool operator !=(LogIn login1, LogIn LogIn2)
        {
            return (login1.Account != LogIn2.Account && login1.Password != LogIn2.Password);
        }

    }
}
