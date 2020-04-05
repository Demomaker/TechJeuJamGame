public class Message 
{
    public string PersonTalkingName;
    public string MessageText;
    public float MessageLengthInSeconds;
    public Message(string personTalkingName, string messageText, float messageLengthInSeconds = 20f) 
    {
        PersonTalkingName = personTalkingName;
        MessageText = messageText;
        MessageLengthInSeconds = messageLengthInSeconds;
    }
}