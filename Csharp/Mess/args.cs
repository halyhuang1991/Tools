using System;

namespace Csharp.Mess
{
    public class args
    {
        public delegate void run(string message);
         public event run t;
        public void echo(string name)
        {
             Console.WriteLine(name);
        }
        public static void GetArgs(string cstord,string deg){
            foreach(var parameter in typeof(args).GetMethod("GetArgs").GetParameters())  
            {  
                 Console.WriteLine(parameter.Name);  
                
            }  
        }
        public static void runAct(Action<string> act){
            //Action<string> act=x=>{Console.WriteLine(x);};
            act.Invoke("ok");
            args a=new args();
            run r=new run(a.echo);
            r("ok");

            //------------
            a.t=a.echo;
            a.t+=a.echo;
            a.t.Invoke("hello");

        }
    }
}