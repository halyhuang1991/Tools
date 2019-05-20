using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Csharp.Design
{
    public class PC{
        public static void run(){
            ProConsume pro=new ProConsume();
            Task task = new Task(pro.consume);
            task.Start();
            for (int i = 0; i < 100; i++)
            {
                Task task1 = new Task(pro.product, "ok"+i);
                task1.Start();
            }
        }
    }
    public class ProConsume
    {
        Queue<string> _tasks = new Queue<string>();
        readonly static object _locker = new object();
        EventWaitHandle _wh = new AutoResetEvent(false);
        
        public void product(object val){
            lock (_locker)
            {
                _tasks.Enqueue((string)val);
            }
            _wh.Set();
        }
        public void consume(){
            while(true){
                List<string> ls=new List<string>();
                lock(_locker){
                    if(_tasks.Count>=10){
                        for(int i=0;i<9;i++){
                            string val=_tasks.Dequeue();
                            ls.Add(val);
                            Console.WriteLine("auto consume product: "+val);
                        }
                    }
                }
                if(ls.Count==0){
                     _wh.WaitOne();
                }
            }
        }
        
    }
}