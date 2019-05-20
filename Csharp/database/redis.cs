using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;
namespace Csharp
{
    public class redis{
        private static readonly double Timeout=1000;
        private static string connstring="Localhost:6379,password=123456";
        /// <summary>
        /// 向Redis写入
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">值</param>
        /// <param name="date">过期日期</param>
        public static void SetString(string key,string value,DateTime? date = null)
        {
            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                //写入
                var db = redis.GetDatabase();
                db.StringSet(key, value);
                //设置过期日期
                
                if (date == null)
                {
                    DateTime time = DateTime.Now.AddSeconds(Timeout);
                    date=time;
                }
                db.KeyExpire(key, date);
                var result = db.StringGet(key);
            }
        }
        /// <summary>
        /// 读取redis的内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                //读取
                var db = redis.GetDatabase();
                var result = db.StringGet(key);
                return result;
            }
        }
        public static string keyRemove(string key)
        {
            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                //读取
                var db = redis.GetDatabase();
                var result = db.StringGet(key);
                db.KeyDelete(key);
                return result;
            }
        }
        public static void SetList(string key,List<string> ls,DateTime? date = null)
        {
            if (ls == null)
            {
                throw new ArgumentNullException(nameof(ls));
            }

            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                //写入
                var db = redis.GetDatabase();
                for (int i = 0; i < ls.Count; i++)
                {
                    db.ListRightPush(key, ls[i]);//从底部插入数据
                }
                if (date == null)
                {
                    DateTime time = DateTime.Now.AddSeconds(Timeout);
                    date=time;
                }
                db.KeyExpire(key, date);
            }
        }
        public static string[] GetList(string key)
        {
            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                //读取
                var db = redis.GetDatabase();
                RedisValue[] result = db.ListRange(key);
                return result.ToStringArray();
            }
        }
        public static void SetHash(string key,Dictionary<string,string> dic,DateTime? date = null){
           List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var d in dic)
            {
                listHashEntry.Add(new HashEntry(d.Key, d.Value));
            }
            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                var db = redis.GetDatabase();
                db.HashSet(key, listHashEntry.ToArray());
                if (date == null)
                {
                    DateTime time = DateTime.Now.AddSeconds(Timeout);
                    date = time;
                }
                db.KeyExpire(key, date);
            }
           
        }
         public static string GetHash(string key,string dkey){
             using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                var db = redis.GetDatabase();
                RedisValue value = db.HashGet(key, dkey);
                return value.ToString();
            }
         }
          public static string[] GetHash(string key,string[] keys){
              RedisValue[] keyarr=keys.ToRedisValueArray();
             using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                var db = redis.GetDatabase();
                RedisValue[] value = db.HashGet(key,keyarr);
                return value.ToStringArray();
            }
         }
         public static Dictionary<string,string> GetHash(string key){
             using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                var db = redis.GetDatabase();
                HashEntry[] value = db.HashGetAll(key);
                return value.ToStringDictionary();
            }
         }
        public static bool transation(string key,string value){
            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                //读取
                var db = redis.GetDatabase();
                string name = db.StringGet(key);
                var tran = db.CreateTransaction();//创建事物
                tran.AddCondition(Condition.StringEqual("name", name));//乐观锁
                tran.StringSetAsync(key, value);
                bool committed = tran.Execute();
                return committed;

            }
        }
        public static void batch(Dictionary<string,string> dic){
            if (dic == null)
            {
                throw new ArgumentNullException(nameof(dic));
            }

            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                //读取
                var db = redis.GetDatabase();
                var batch = db.CreateBatch();
                foreach(var d in dic){
                     Task t = batch.StringSetAsync(d.Key, d.Value);
                }
                batch.Execute();
            }
        }
        public static void Lock(string key,Action a){
            RedisValue token = Environment.MachineName;
            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                //读取
                var db = redis.GetDatabase();
                if (db.LockTake(key, token, TimeSpan.FromSeconds(10)))
                {
                    try
                    {
                        a();
                    }
                    finally
                    {
                        db.LockRelease(key, token);//释放锁
                    }
                }

            }
        }
        public static string Lock(string  key,string value,Func<string> a){
            RedisValue token = Environment.MachineName;
            string msg="";
            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                //读取
                var db = redis.GetDatabase();
                bool t=db.LockTake(key, value, TimeSpan.FromSeconds(100));
                if (t)
                {
                    try
                    {
                        bool flg=true;
                        while(flg){
                            string r=a();
                            if(r!=""){
                                flg=false;
                                msg=r;
                            }
                        }
                        
                    }
                    finally
                    {
                        db.LockRelease(key, value);//释放锁
                    }
                }

            }
            return msg;
        }
        public static void Publish(string key,string value){
            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
               ISubscriber sub = redis.GetSubscriber();
                sub.Publish(key, value);
            }
        }
         public static void Subscribe(string key,Action<string> f){
            using (var redis = ConnectionMultiplexer.Connect(connstring))
            {
                ISubscriber sub = redis.GetSubscriber();
                sub.Subscribe(key, new Action<RedisChannel, RedisValue>((channel, message) =>
                {
                    f(message);
                }));
            }
        }
    }
}