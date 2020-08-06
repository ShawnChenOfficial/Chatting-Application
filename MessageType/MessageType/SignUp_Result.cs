using System;
namespace MessageType
{
    [Serializable]
    public class SignUp_Result
    {
        public bool Result { get; set; }
        public SignUp Signup { get; set; }

        public SignUp_Result(bool result, SignUp signUp)
        {
            this.Result = result;
            this.Signup = signUp;
        }
    }
}
