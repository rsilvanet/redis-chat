using RedisChat.Core;
using System;

namespace RedisChat.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connection = new Connection("localhost:6379", 0);
            var subscriber = connection.Redis.GetSubscriber();

            subscriber.Subscribe("chat", (channel, message) =>
            {
                Console.WriteLine(message);
            });

            while (true)
            {
                var text = Console.ReadLine();

                if (text == "/quit")
                {
                    break;
                }

                subscriber.Publish("chat", text);
            }
        }
    }
}
