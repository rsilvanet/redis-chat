using Newtonsoft.Json;
using RedisChat.Core;
using System;

namespace RedisChat.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var guid = Guid.NewGuid().ToString();
            var connection = new Connection("localhost:6379", 0);
            var subscriber = connection.Redis.GetSubscriber();

            subscriber.Subscribe("chat", (channel, json) =>
            {
                var message = JsonConvert.DeserializeObject<SimpleMessage>(json);

                if (message.Source != guid)
                {
                    Console.WriteLine($"{message.Source} typed: {message.Content}");
                }
            });

            Console.WriteLine($"Hello. Your id is: {guid}");
            Console.WriteLine();

            while (true)
            {
                var text = Console.ReadLine();

                if (text == "/quit")
                {
                    break;
                }

                var message = new SimpleMessage(guid, text);
                var json = JsonConvert.SerializeObject(message);

                subscriber.Publish("chat", json);
            }
        }
    }
}
