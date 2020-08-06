using System;
namespace MessageType
{
    [Serializable]
    public class Message_Sent_Result
    {
        public bool Result { get; set; }
        public string ErrorMessage { get; set; }
        public Message_Sent_Result(bool result, string errorMessage)
        {
            this.Result = result;
            this.ErrorMessage = errorMessage;
        }
    }
}
