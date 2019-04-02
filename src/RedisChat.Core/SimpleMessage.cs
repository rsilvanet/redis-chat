namespace RedisChat.Core
{
    public class SimpleMessage
    {
        public SimpleMessage(string session, string username, string content)
        {
            Session = session;
            Username = username;
            Content = content;
        }

        public string Session { get; set; }

        public string Username { get; set; }

        public string Content { get; set; }
    }
}
