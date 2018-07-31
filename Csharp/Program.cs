using System;
using System.Collections.Generic;
using System.IO;
using Csharp.Models;

namespace Csharp
{
    class Program
    {
        static void Main(string[] args)
        {
           MongoDbGo();
           MongoDbDo();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
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
              MongoDb tl = new MongoDb(arr,"MongoDB");
              users user=new users();
              user.Name ="haly123";
              user.Sex ="ok";
              //tl.Insert<users>(user);
              List<users> ls=tl.Query<users>();
              foreach(users str in ls){
                  Console.WriteLine(str.Name);
              }
             

        }
    }
}
