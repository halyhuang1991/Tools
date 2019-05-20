using System;
using System.Collections.Generic;

namespace Csharp.Design
{

    public class staticMethod
    {
        public static List<string> ls = null;
        static staticMethod()
        {//不管新建实例还是静态方法 都会有的
            if (ls == null)
            {
                ls = new List<string>();
                Console.Write("staticMethod!\r\n");
            }
        }
        
        public static void log()
        {
            Console.WriteLine("ok");
        }
        //--------------------------
        // var s = new Csharp.Design.staticMethod();
        // s.log1();
        public staticMethod(){
            Console.Write("233\r\n");
        }
        public staticMethod(string msg):this()
        {//相当于提前执行了this()
            Console.Write("2331\r\n");
        }
        public void log1(){
            Console.WriteLine("new class");
        }

    }
    public class ext:staticMethod{
        public ext(string msg):base(msg){
           Console.WriteLine("ext");
           Console.WriteLine(msg);
        }
    }
    
}