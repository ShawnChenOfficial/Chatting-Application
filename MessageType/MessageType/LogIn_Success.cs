using System;
namespace MessageType
{
    [Serializable]
    public class LogIn_Result
    {
        public bool Result { get; set; }
        public LogIn Login { get; set; }

        public LogIn_Result(bool result, LogIn logIn)
        {
            this.Result = result;
            this.Login = logIn;
        }
    }
}
