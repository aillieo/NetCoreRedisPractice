using StackExchange.Redis;
using System;

namespace NetCoreRedisPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379"))
            {
                IDatabase db = redis.GetDatabase();
                string value = "myvalue";
                db.StringSet("mykey", value);
                Console.WriteLine(db.StringGet("mykey"));

                ISubscriber sub = redis.GetSubscriber();
                sub.Subscribe("messages", (channel, message) => {
                    Console.WriteLine((string)message);
                });
                string messages = "mymessages";
                sub.Publish("messages", messages);

                Console.ReadLine();
            }
        }
    }
}
