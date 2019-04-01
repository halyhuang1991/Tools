using System;
using System.Collections.Generic;
using System.IO;
using Csharp.Models;
using Csharp.database;
using Csharp.Mess;

namespace Csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            SendMail.sendingMail();
            Console.WriteLine("\r\nHello World!");
            //Console.ReadKey();
        }
        
       
        private static void DesignPublish(){
            Publish p = new Publish();
            Subscriber sub = new Subscriber("one");
            Subscriber sub1 = new Subscriber("Two");
            p.AddObserver(sub);
            p.AddObserver(sub1);
            p.Update();
            p.RemoveObserver(sub1);
            Console.WriteLine("Remove Observer!");
            p.Update();
        }
        private static void redisGo(){
             redis.SetString("key", "value123");
             Console.WriteLine(redis.GetString("key2"));
             Console.WriteLine(redis.GetString("2"));
             //----------------------------
            Console.WriteLine(redis.GetString("22"));
            PooledRedis.GetInstance.SetString("key2", "value123");
            Console.WriteLine(PooledRedis.GetInstance.GetString("key2"));
        }
         private static void ssredisGo(){
            var ssredis=new RedisHelper();
            ssredis.Set<string>("NServiceKit","ok");
            ssredis.Subscribe("sds","sd");
         }
        private static  void MongoDbGo(){
            MongoFileBll tl = new MongoFileBll("mongodb://127.0.0.1:27017/MongoDB");
            string folder = @"C:\Users\halyhuang\Desktop\temp\txt";
            DirectoryInfo di = new DirectoryInfo(folder);
            foreach (var file in di.GetFiles())
            {
                //tl.UploadFile(file.FullName, Guid.NewGuid().ToString());
            }
            //tl.DeleteFile("b2be8ec8-8b6b-44c9-9efb-229f71ca3685");
            var aaa = tl.FindAll();
            foreach (var inst in aaa)
            {
                Console.WriteLine(inst.Name);
            }
        }
        private static  void MongoDbDo(){
              string[] arr=new string[1]{"127.0.0.1:27017"};
             // arr=new string[3]{"127.0.0.1:27016","127.0.0.1:27018","127.0.0.1:27019"};
              MongoDb tl = new MongoDb(arr,"MongoDB");
              users user=new users();
              user.Name ="haly123";
              user.Sex ="ok";
              tl.Insert<users>(user);
              List<users> ls=tl.Query<users>();
              foreach(users str in ls){
                  Console.WriteLine(str.Name);
              }
             

        }
    }
}
