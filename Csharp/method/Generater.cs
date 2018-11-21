using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Csharp.Models;

namespace Csharp.method
{
    public class TestGenerater{
        public static void run(){
            Generater generater=new Generater();
            Task[] task=new Task[100];
            for(int i=0;i<100;i++){
                book book=new book();
                book.id=i;
                // if(i%2==0){
                //     task[i]=new Task(generater.Producer,(object)book);
                // }else{
                //     task[i]=new Task(generater.consume);
                // }
                if(i%2==0){
                    task[i]=new Task(generater.Producerbk,(object)book);
                }else{
                    book.id=i-1;
                    task[i]=new Task(generater.consumebk,(object)book);
                }
                task[i].Start();
            }
            Task.WaitAll(task);
        }
         public static void run1(){
             Generater1 generater=new Generater1();
         }
    }
    public class Generater1{
         private Queue _tasks=new Queue();
        readonly object _locker = new object();
        EventWaitHandle _wh = new AutoResetEvent(false);
        Thread _worker;
        public Generater1(){
            var cityIds = new List<int> {101280601, 101010100, 101020100, 101110101, 101040100};
            
            // 任务开始，启动工作线程
            _worker = new Thread(Work);
            _worker.Start((object)cityIds.Count);

            // 生产者将数据插入队里中，并给工作线程发信号
            foreach (var cityId in cityIds)
                EnqueueTask(FetchData(cityId));
            EnqueueTask(null);
        }
        void EnqueueTask(string task)
        {
            lock (_locker) 
                _tasks.Enqueue(task);  // 向队列中插入任务 
            
            _wh.Set();  // 给工作线程发信号
        }
        void Work(object o)
        {
            int count =(int)o;
            bool t=true;
            int i=0;
            while (t)
            {
                string work = null;
                lock (_locker)
                {
                    if (_tasks.Count > 0)
                    {
                        work = _tasks.Dequeue() as string; // 有任务时，出列任务
                        
                        if (work == null)  // 退出机制：当遇见一个null任务时，代表任务结束
                            return;
                    }
                }

                if (work != null){
                    i++;
                    SaveData(work);  // 任务不为null时，处理并保存数据
                } 
                else
                    _wh.WaitOne();   // 没有任务了，等待信号
                if(i==count){
                    //t=false;
                }
            }
        }
        string FetchData(int cityId)
        {
            return cityId.ToString();
        }
        void SaveData(string data)
        {
            Console.WriteLine("Save Data!"+data);
            Thread.Sleep(2000);  // 模拟数据保存
        }
    }
    public class Generater
    {
        private Queue myq = new Queue();
        private List<book> bk=new List<book>();
       
        public void Producer(object o)
        {
            book b=o as book;
            lock (myq.SyncRoot)
            {
                myq.Enqueue(b);
                Console.WriteLine($"...generate {b.id}....");
            }

        }
        public void consume()
        {
            bool t = true;
            Stopwatch getTime=new Stopwatch();
            getTime.Start();
            while (t)
            {//挂起 等待
                if (myq.Count > 0)
                {
                    lock (myq.SyncRoot)
                    {
                        if (myq.Count > 0)
                        {//最先 但没有可以消费的book
                            object o = myq.Dequeue();
                            book b = o as book;
                            Console.WriteLine($"...consume {b.id}....");
                            t = false;//消费了
                        }
                    }
                }
                double second=new TimeSpan(getTime.ElapsedMilliseconds).TotalSeconds;
                if(second>10){
                     t = false;
                }

            }
            getTime.Stop();
            
        }
        public void ConsumeAll()
        {
            lock (myq.SyncRoot)
            {
                while(myq.Count>0){
                    myq.Dequeue();
                }
                Console.WriteLine("...consume all....");
            }
        }
        public void Producerbk(object o)
        {
            book b=o as book;
            lock (bk)
            {
                bk.Add(b);
                Console.WriteLine($"...generate {b.id}....");
            }

        }
        public void consumebk(object o)
        {
            book b = o as book;
            bool t=true;
            while (t)
            {
                lock (bk)
                {
                    if (bk.Find(x => x.id == b.id) != null)
                    {
                        bk.Remove(b);
                        Console.WriteLine($"...consume {b.id}....");
                        t=false;
                    }

                }
            }
            
        }
    }
}