using System;
using System.Threading;
using System.Threading.Tasks;
using Csharp.database;

namespace Csharp.Mess
{
    public class RedisLock
    {
       
        public void MultiLock(){
            string val=Csharp.redis.Lock("keyid","ok",()=>{
               return Csharp.redis.GetString("msgkeyid");
            });
            Console.WriteLine(val);
            RedisHelper redisHelper=new RedisHelper();
            bool t=redisHelper.Lock("keyid","ok");
            if(t){
                Console.WriteLine(redisHelper.Get<string>("keyid"));
                redisHelper.Remove("keyid");
                Console.WriteLine("unlock and delete");
            }else{
                 Console.WriteLine("can not lock");
            }
            
        }
        public static void test(){
            RedisLock redisLock=new RedisLock();
            Task task=new Task(redisLock.MultiLock);
            task.Start();
            Console.WriteLine("wait ten seconds.");
            Thread.Sleep(10);
            Csharp.redis.SetString("msgkeyid","ok");
            Console.Read();
        }
    }
}