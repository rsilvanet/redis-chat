using StackExchange.Redis;

namespace RedisChat.Core
{
    public class Connection
    {
        public Connection(string address, int db)
        {
            Redis = ConnectionMultiplexer.Connect(address);
            Database = Redis.GetDatabase(db);
        }

        public IConnectionMultiplexer Redis { get; set; }

        public IDatabase Database { get; set; }
    }
}
