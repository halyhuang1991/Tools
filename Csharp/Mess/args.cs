using System;

namespace Csharp.Mess
{
    public class args
    {
        public static void GetArgs(string cstord,string deg){
            foreach(var parameter in typeof(args).GetMethod("GetArgs").GetParameters())  
            {  
                 Console.WriteLine(parameter.Name);  
                
            }  
        }
        public static void runAct(Action<string> act){
            //Action<string> act=x=>{Console.WriteLine(x);};
            act.Invoke("ok");
        }
    }
}