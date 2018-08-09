using Newtonsoft.Json;
using NServiceKit.Redis;
using System;
using System.Collections.Generic;

namespace Csharp.database
{
    
    public class RedisHelper : IDisposable
        {
            
        private RedisClient Redis = new RedisClient("127.0.0.1", 6379);
        //缓存池
        PooledRedisClientManager prcm = new PooledRedisClientManager();
        //默认缓存过期时间单位秒
        public int secondsTimeOut = 20 * 60;
        /// <summary>
        /// 缓冲池
        /// </summary>
        /// <param name="readWriteHosts"></param>
        /// <param name="readOnlyHosts"></param>
        /// <returns></returns>
        public static PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts,
            new RedisClientManagerConfig
            {
            MaxWritePoolSize = readWriteHosts.Length * 5,
            MaxReadPoolSize = readOnlyHosts.Length * 5,
            AutoStart = true,
            }); 
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="OpenPooledRedis">是否开启缓冲池</param>
        public RedisHelper(bool OpenPooledRedis = false)
        {
            if (OpenPooledRedis)
            {
            prcm = CreateManager(new string[] { "127.0.0.1:6379" }, new string[] { "127.0.0.1:6379" });
            Redis = prcm.GetClient() as RedisClient;
            }
        }
        /// <summary>
        /// 距离过期时间还有多少秒
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long TTL(string key)
        {
            return Redis.Ttl(key);
        }
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeout"></param>
        public void Expire(string key,int timeout = 0)
        {
            if (timeout >= 0)
            {
            if (timeout > 0)
            {
            secondsTimeOut = timeout;
            }
            Redis.Expire(key, secondsTimeOut);
            }
        }
        #region Key/Value存储
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存建</param>
        /// <param name="t">缓存值</param>
        /// <param name="timeout">过期时间，单位秒,-1：不过期，0：默认过期时间</param>
        /// <returns></returns>
        public bool Set<T>(string key, T t, int timeout = 0)
        {
            Redis.Set<T>(key, t);
            if (timeout >= 0)
            {
            if (timeout > 0)
            {
            secondsTimeOut = timeout;
            }
            Redis.Expire(key, secondsTimeOut);
            }
            return true;

        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return Redis.Get<T>(key);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Redis.Remove(key);
        }
        public bool exists(string key)
        {
            return Redis.ContainsKey(key);
        }
        public bool SetHash<T>(string hash,string key,T body)where T:class{
            this.Redis.SetEntryInHash(hash, key, JsonConvert.SerializeObject(body));
            return true;
        }
        public List<string> GetHash(string hash){
            return this.Redis.GetHashValues(hash);
        }
        public bool SetList<T>(string key,T body)where T:class{
            this.Redis.AddItemToList(key, JsonConvert.SerializeObject(body));
            return true;
        }
         public List<string> GetList(string key){
            return this.Redis.GetAllItemsFromList(key);
        }
        public bool SetQueue<T>(string key,T body)where T:class{
            this.Redis.EnqueueItemOnList(key, JsonConvert.SerializeObject(body));
            return true;
        }
         public bool DeQueue(string key){
            while (this.Redis.GetListCount(key) > 0)
            {
                Console.WriteLine(this.Redis.DequeueItemFromList(key));
            }
            return true;
        }
         public bool transation(string key,string value){
             Action act=()=>{
                this.Redis.Set<string>(key,value);
             };
            var tran=this.Redis.CreateTransaction();
            tran.CompleteVoidQueuedCommand(act);
            bool committed = tran.Commit();
            return committed;
        }
        public void Subscribe(string key,string value){
            
            var sub1=this.Redis.CreateSubscription();
            sub1.OnMessage = (chanel, message) =>{
                           Console.WriteLine("chanel is :{0}", chanel);
                           Console.WriteLine("message is :{0}", message);
                     };
            sub1.SubscribeToChannels(new string[] { "msg" });
            
        }
        
        public bool Publish(){
           this.Redis.PublishMessage("msg","ok121");
           return true;
        }
        #endregion
        //释放资源
        public void Dispose()
        {
            if (Redis != null)
            {
            Redis.Dispose();
            Redis = null;
            }
            GC.Collect();
            }
        }
}