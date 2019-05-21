using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Csharp.Mess
{
    public class HashLock
    {//单线程写，多线程读
        private Hashtable cht = Hashtable.Synchronized(new Hashtable());
        public HashLock()
        {

        }
        public void AddOrUpdate(string key, dynamic value)
        {
            lock (cht.SyncRoot)
            {
                if (!cht.ContainsKey(key))
                {
                    cht.Add(key, value);
                }
                else
                {
                    cht[key] = value;
                }
            }
        }
        public object QueryValue(string key)
        {
            return cht[key];
        }
        public List<object> QueryAll()
        {
            List<object> ls = new List<object>();
            lock (cht.SyncRoot)
            {
                foreach (object o in cht)
                {
                    ls.Add(o);
                }
            }
            return ls;
        }
        private static int random(int a,int b){
           Random ran=new Random();int num=ran.Next(a, b);
           return num;
        }
        public static void test(){
            HashLock localLock = new HashLock();
            for (int i = 0; i < 10; i++)
            {
                Action<int> action = (ii) =>
                {
                    Task task = new Task(() =>
                    {
                        localLock.AddOrUpdate("key"+random(0,10), "value" + ii);
                    });
                    task.Start();
                };
                action(i);
                Task<object> task1 = new Task<object>(() =>
                    {
                        object o=localLock.QueryValue("key"+random(0,10));
                        return o==null?"null":o;
                    });
                 task1.Start();task1.Wait();
                 Console.WriteLine(task1.Result.ToString());
                 Task.Run(()=>{
                        List<object> list=localLock.QueryAll();
                        Console.WriteLine(list.Count);
                    });
            }
            Console.Read();
        }
    }
}