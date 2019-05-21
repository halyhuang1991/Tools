using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Csharp.helpers
{
    public class LocalLock
    {
        private static ConcurrentDictionary<string, object> _LockCache = new ConcurrentDictionary<string, object>();
        private static ConcurrentDictionary<string, object> _LockUserCache = new ConcurrentDictionary<string, object>();
        private bool IsNullAndNotEmpty(string key, string val)
        {
            return string.IsNullOrEmpty(val) || string.IsNullOrEmpty(val);
        }
        /// 

        /// 获取一个锁(需要自己释放)
        /// 

        /// 锁的键
        /// 当前占用值
        /// 耗时时间
        /// 成功返回true
        public bool LockTake(string key, string value, TimeSpan span)
        {
            // bool ret = true;
            // ret = IsNullAndNotEmpty(key, value);
            // if (!ret) return false;
            var obj = _LockCache.GetOrAdd(key, (string x) => { return new object(); });
            if (Monitor.TryEnter(obj, span))
            {
                _LockUserCache[key] = value;
                return true;
            }
            return false;
        }

        /// 

        /// 异步获取一个锁(需要自己释放)
        /// 

        /// 锁的键
        /// 当前占用值
        /// 耗时时间
        /// 成功返回true
        public Task LockTakeAsync(string key, string value, TimeSpan span)
        {
            return Task.FromResult(LockTake(key, value, span));
        }

        /// 

        /// 释放一个锁
        /// 

        /// 锁的键
        /// 当前占用值
        /// 成功返回true
        public bool LockRelease(string key, string value)
        {
            // bool ret = true;
            // ret = IsNullAndNotEmpty(key, value);
            // if (!ret) return false;
            object obj;
            _LockCache.TryGetValue(key, out obj);
            if (obj != null)
            {
                if ((string)_LockUserCache[key] == value)
                {
                    Monitor.Exit(obj);
                    return true;
                }
                return false;
            }
            return true;
        }

        /// 

        /// 异步释放一个锁
        /// 

        /// 锁的键
        /// 当前占用值
        /// 成功返回true
        public Task LockReleaseAsync(string key, string value)
        {
            return Task.FromResult(LockRelease(key, value));
        }

        /// 

        /// 使用锁执行一个方法
        /// 

        /// 锁的键
        /// 当前占用值
        /// 耗时时间
        /// 要执行的方法
        public void ExecuteWithLock(string key, string value, TimeSpan span, Action<string,string> executeAction,Action FailedAction=null)
        {
            if (executeAction == null) return;
            if (LockTake(key, value, span))
            {
                try
                {
                    executeAction(key, value);
                }
                finally
                {
                    LockRelease(key, value);
                }
            }
            else
            {

                if (FailedAction != null)
                {
                    FailedAction();
                }

            }
        }
        public void ExecuteWithLock(string key, string value, TimeSpan span, Action executeAction)
        {
            if (executeAction == null) return;
            if (LockTake(key, value, span))
            {
                try
                {
                    executeAction();
                }
                finally
                {
                    LockRelease(key, value);
                }
            }
            
        }
    }
}