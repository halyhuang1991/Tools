using System;

namespace Csharp.method
{
    public delegate void runAct(string msg);
    public class dele
    {
        public void logstr(string str){
            Console.WriteLine(str);
        }
         public void logstr1(string str){
            Console.WriteLine(str+"12");
        }
        public static void run(){
            var n=new dele();
            runAct Act=n.logstr;
            Act+=n.logstr1;
            //Action<string> ok=(msg)=>{Console.WriteLine(msg);};
            Act("ok");

        }
    }
}