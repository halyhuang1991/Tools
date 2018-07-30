using System;
using Newtonsoft.Json;
using StackExchange.Redis;
namespace Csharp
{
    public class PooledRedis
    {
        private static readonly double Timeout=1000;
        private static object _objLock = new object();
        private static PooledRedis _dataService = null;
        private static IDatabase db;
        public static PooledRedis GetInstance
        {
            get
            {
                if (_dataService == null)
                {
                    lock (_objLock)
                    {
                        if (_dataService == null)
                        {
                            ConfigurationOptions option = new ConfigurationOptions();
                            option.EndPoints.Add("Localhost:6379");
                            option.Password="BG84876008";
                            option.ServiceName = "127.0.0.1";//只能对master库写缓存,从库只读或slave-read-only no
                            var _redis = ConnectionMultiplexer.Connect(option);
                            option.CommandMap = CommandMap.Sentinel;//主服务器挂了从服务器做主
                            db = _redis.GetDatabase();
                            _dataService = new PooledRedis();
                        }
                    }
                }
                return _dataService;
            }
        }
        public void SetString(string key,string value,DateTime? date = null)
        {
                db.StringSet(key, value);
                //设置过期日期
                if (date == null)
                {
                    DateTime time = DateTime.Now.AddSeconds(Timeout);
                    date=time;
                }
                db.KeyExpire(key, date);
        }
        public string GetString(string key)
        {
            var result = db.StringGet(key);
            return result;
        }
        public T Getobject<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(db.StringGet(key));
        }
        public string[] GetList(string key)
        {
            RedisValue[] result = db.ListRange(key);
            return result.ToStringArray();
        }


    }
}