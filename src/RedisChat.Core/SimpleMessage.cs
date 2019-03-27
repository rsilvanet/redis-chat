namespace RedisChat.Core
{
    public class SimpleMessage
    {
        public SimpleMessage(string source, string content)
        {
            Source = source;
            Content = content;
        }

        public string Source { get; set; }

        public string Content { get; set; }
    }
}
