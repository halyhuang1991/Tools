using System;
using Csharp.Mess;

namespace Csharp.method
{
    public delegate void runAct(string msg);
    
    public class dele
    {
        public event runAct BlogPublished;
        public void logstr(string str){
            Console.WriteLine(str);
            if(BlogPublished!=null)BlogPublished(str);
        }
         public void logstr1(string str){
            Console.WriteLine(str+"12");
        }
        public void logstr3(string str){
            Console.WriteLine(str+"123");
        }
        public static void run(){
           
            var n=new dele();
            n.BlogPublished+=n.logstr3;
            runAct Act=n.logstr;
            Act+=n.logstr1;
            //Action<string> ok=(msg)=>{Console.WriteLine(msg);};
            Act("ok");

        }
    }
}