using System;

namespace Csharp
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
        private void redisGo(){
            redis.SetString("key", "value123");
             Console.WriteLine(redis.GetString("key2"));
             Console.WriteLine(redis.GetString("2"));
             //----------------------------
            Console.WriteLine(redis.GetString("22"));
            PooledRedis.GetInstance.SetString("key2", "value123");
            Console.WriteLine(PooledRedis.GetInstance.GetString("key2"));
        }
    }
}
