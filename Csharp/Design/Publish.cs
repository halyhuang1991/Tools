using System;
using System.Collections.Generic;

namespace Csharp
{
    public class Publish
    {
        private List<IObserver> Observers=new List<IObserver>();
        public string GetMsg(IObserver ob){
            return "ok";
        }
        public void AddObserver(IObserver ob)
        {
            Observers.Add(ob);
        }
        public void RemoveObserver(IObserver ob)
        {
            Observers.Remove(ob);
        }
        public void Update(){
            foreach (IObserver ob in Observers)
            {
                if (ob != null)
                {
                    ob.Receive(GetMsg(ob));
                }
            }
        }
    }
    public interface IObserver
    {
        void Receive(string msg);
    }
    public class Subscriber : IObserver
    {
        public string Name { get; set; }
        public Subscriber(string name)
        {
            this.Name = name;
        }

        public void Receive(string msg)
        {
            Console.WriteLine("订阅者 {0} 观察到了{1}", Name, msg);
        }
    }
}