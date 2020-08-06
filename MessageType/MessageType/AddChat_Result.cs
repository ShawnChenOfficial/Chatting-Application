using System;
namespace MessageType
{
    [Serializable]
    public class AddChat_Result
    {
        public AddChat Addchat { get; set; }
        public bool Result { get; set; }

        public AddChat_Result(bool result, AddChat addChat)
        {
            this.Result = result;
            this.Addchat = addChat;
        }
    }
}
