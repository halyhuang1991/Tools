using System;
using System.Collections.Generic;
using System.Reflection;

namespace Csharp.Mess
{
     public interface IMiddleWare{
         void Execute();
     }
     public class testAuth:IMiddleWare{
         public void Execute(){
             Console.WriteLine("testAuth");
         }
     }
     public class testCache:IMiddleWare{
         public void Execute(){
             Console.WriteLine("testCache");
         }
     }
    public class Event1
    {
        
        public event EventHandler BlogPublished;
        public event Action<object, EventArgs> EventName;
        private List<IMiddleWare> middleWare=new List<IMiddleWare>();
        public void use(IMiddleWare Ware){
            middleWare.Add(Ware);
        }
        public void Published(object sender, EventHandler handler)
        {
            Console.WriteLine("test");
        }
        public void go()
        {
            Console.WriteLine("Trigger an event.");
            if (BlogPublished != null)
                BlogPublished(this, null);
        }
       public void Execute(object sender, EventArgs args)
        {
             Console.WriteLine(" Execute test");
            if (middleWare != null)
            {
                foreach (IMiddleWare m in middleWare)
                {
                    m.Execute();
                }
            }
             
        }
        //Owner的事件eventName执行了就执行this 的Execute 方法
        public void BindEvent(object Owner, string eventName)
        {
            var Event = Owner.GetType().GetEvent(eventName, BindingFlags.Public | BindingFlags.Instance);
            if (Event == null)
                throw new InvalidOperationException(String.Format("你绑定的事件名有错误: {0}", eventName));
            var _methodInfo = this.GetType().GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance);
            //Create an event handler for the event that will call the ExecuteCommand method
            //EventHandler = EventHandlerGenerator.CreateDelegate(
            //    Event.EventHandlerType, _methodInfo, this);
            var EventHandler = Delegate.CreateDelegate(Event.EventHandlerType, this, _methodInfo);
             //Register the handler to the Event
            Event.AddEventHandler(Owner, EventHandler);
        }
        public static void Run(){
            //  var e=new Event1();
            //  e.use(new testAuth()); e.use(new testCache());
            // //e.BlogPublished+=delegate { Console.WriteLine("Hello, World!"); };
            // e.BindEvent(e,"BlogPublished");
            // e.go();

            var t=Type.GetType("Csharp.Mess.Event1");
            object obj = Activator.CreateInstance(t);
            MethodInfo bind=t.GetMethod("BindEvent");
            bind.Invoke(obj,new object[]{obj,"BlogPublished"});
            MethodInfo go=t.GetMethod("go");
            go.Invoke(obj,new object[]{});
            
        }
    }
}