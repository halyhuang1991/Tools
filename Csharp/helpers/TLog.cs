using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Csharp.helpers
{
    public class TLog
    {
        public static void Test()
        {
            int location=0;
            for (int i = 0; i < 30; i++)
            {
                Task.Run(() =>
                {
                    Interlocked.Increment(ref location);
                    TLog.Write($"\r\n{location} test {DateTime.Now.ToString("yyyy-MM-dd hhhmmssfff") }");
                    Thread.Sleep(1);
                });
                Task.Run(() =>
                {
                    TLog.read();
                    //Console.WriteLine(TLog.read());
                });
            }
            Console.Read();

        }
        //防止读的时候其他线程写，允许读的时候其他线程读
        private static ReaderWriterLockSlim rwl = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        public static void WriteLog(string str)
        {

        }
        public static void Write(string str)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string filePath = System.Environment.CurrentDirectory + "\\logs";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath += "\\" + fileName;
           
            try
            {
                rwl.TryEnterWriteLock(5);//如果有其他锁等5秒进入
                 //rwl.ExitWriteLock();
                File.AppendAllText(filePath, "\r\n" + str);
            }
            finally
            {
                if(rwl.IsWriteLockHeld)
                  rwl.ExitWriteLock();
            }

        }
        public static string read()
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string filePath = System.Environment.CurrentDirectory + "\\logs";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath += "\\" + fileName;
            string err = "";
            if (!File.Exists(filePath)) return err;
            try
            {
                rwl.TryEnterReadLock(5);//如果有写，等5秒后进入
                err = File.ReadAllText(filePath);
            }
            finally
            {
                rwl.ExitReadLock();
            }
            return err;
        }
        public void Update()
        {
            if (rwl.TryEnterUpgradeableReadLock(100))
            {
                try
                {
                    //读操作
                    rwl.EnterWriteLock();
                    try
                    {
                        //写操作
                    }
                    finally
                    {
                        rwl.ExitWriteLock();
                    }
                }
                finally
                {
                    rwl.ExitUpgradeableReadLock();
                }
            }
        }
    }
}