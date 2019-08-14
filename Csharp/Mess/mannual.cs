using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Csharp.Mess
{
    public class mannual
    {
        private static ManualResetEvent manualReset=new ManualResetEvent(false);
        private volatile int count =0;
        private static int icount=0;
        public void product(){
             while(count>0){
                 manualReset.WaitOne();
                 
             }
             count++;
             Console.WriteLine($"product one,count is {count}");
             icount++;
             manualReset.Set();
        }
        public void resume(){
            while(count==0){
                manualReset.WaitOne();
            }
             count--;
             Console.WriteLine($"resume one,count is {count}");
             icount++;
             manualReset.Set();
        }
        public static void Run(){
            mannual mannual=new mannual();
            Task[] tasks=new Task[60];
            for(int i=0;i<30;i++){
                // Task.Run(()=>mannual.resume());
                // Task.Run(()=>mannual.product());
                tasks[2*i]=new Task(()=>mannual.resume());
                tasks[2*i+1]=new Task(()=>mannual.product());
            }
            tasks.ToList().ForEach(x=>{x.Start();});
            //Task.Run(()=>mannual.resume());
            //Console.ReadLine();
           
            Task.WhenAll(tasks).ContinueWith(p =>
            {
                
                Console.WriteLine($"run {mannual.icount}");
            }, TaskContinuationOptions.OnlyOnRanToCompletion).Wait();
            Console.ReadLine();
            
        }
    }
}