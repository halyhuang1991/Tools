using System;
using System.Threading;
using System.Threading.Tasks;

namespace Csharp.method
{
    public class Task1
    {
        public void test(){
            Console.WriteLine("111 balabala. My Thread ID is :" + Thread.CurrentThread.ManagedThreadId);
            AsyncMethod();
            var ResultTask  = AsyncMethod1();
            Console.WriteLine(ResultTask.Result);
            Console.WriteLine("222 balabala. My Thread ID is :" + Thread.CurrentThread.ManagedThreadId);
        }
         private async Task<string> AsyncMethod1()
        {
            var ResultFromTimeConsumingMethod = TimeConsumingMethod();
            string Result = await ResultFromTimeConsumingMethod + " + AsyncMethod1. My Thread ID is :" + Thread.CurrentThread.ManagedThreadId;
            string r=await TimeConsumingMethod1();
            Console.WriteLine(Result);
            return Result+"---"+r;
        }
        private async Task AsyncMethod()
        {
            var ResultFromTimeConsumingMethod = TimeConsumingMethod();
            string Result = await ResultFromTimeConsumingMethod + " + AsyncMethod. My Thread ID is :" + Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(Result);
            //返回值是Task的函数可以不用return
        }
        private Task<string> TimeConsumingMethod1()
        {
            var task = Task.Run(() =>
            {
                Console.WriteLine("TimeConsumingMethod1");
                return "ok";
            });
            return task;
        }
        //这个函数就是一个耗时函数，可能是IO操作，也可能是cpu密集型工作。
        private Task<string> TimeConsumingMethod()
        {            
            var task = Task.Run(()=> {
                Console.WriteLine("Helo I am TimeConsumingMethod. My Thread ID is :" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(15000);
                Console.WriteLine("Helo I am TimeConsumingMethod after Sleep(5000). My Thread ID is :" + Thread.CurrentThread.ManagedThreadId);
                return "Hello I am TimeConsumingMethod";
            });

            return task;
        }
    }
}