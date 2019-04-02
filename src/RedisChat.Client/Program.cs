using Newtonsoft.Json;
using RedisChat.Core;
using System;

namespace RedisChat.Client
{
    public class Program
    {
        private static string _user;
        private static string _channel;
        private static string _session;

        public static void Main(string[] args)
        {
            _session = Guid.NewGuid().ToString();

            Console.Write("Choose an username: ");
            _user = Console.ReadLine();

            Console.Write("Choose a channel: ");
            _channel = Console.ReadLine();

            var connection = new Connection("localhost:6379", 0);
            var subscriber = connection.Redis.GetSubscriber();

            subscriber.Subscribe(_channel, (channel, json) =>
            {
                var message = JsonConvert.DeserializeObject<SimpleMessage>(json);

                if (message.Session != _session)
                {
                    Console.WriteLine($"{message.Username} typed: {message.Content}");
                }
            });

            Console.WriteLine($"Hello {_user}.");
            Console.WriteLine($"Welcome to the {_channel} channel.");

            while (true)
            {
                var text = Console.ReadLine();

                if (text == "/quit")
                {
                    break;
                }

                var message = new SimpleMessage(_session, _user, text);
                var json = JsonConvert.SerializeObject(message);

                subscriber.Publish(_channel, json);
            }
        }
    }
}
