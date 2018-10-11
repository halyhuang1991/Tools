using System;
using System.Reflection;

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
        public static void invoke1(){
            string className = "Csharp.Mess.ClassSample";
            string methodName = "test1";
            //传递参数
            Object[] paras = new Object[] { "我的", "电脑" };
            var t = Type.GetType(className);
            object obj = Activator.CreateInstance(t);

            try
            {
                #region 方法一
                //直接调用
                MethodInfo method = t.GetMethod("test2");
                method.Invoke(obj, paras);
                //method.Invoke(any, paras);//异常：必须实例化反射要反射的类，因为要使用的方法并不是静态方法
                #endregion

                #region 方法二
                MethodInfo[] info = t.GetMethods();
                for (int i = 0; i < info.Length; i++)
                {
                    var md = info[i];
                    //方法名
                    string mothodName = md.Name;
                    //参数集合
                    ParameterInfo[] paramInfos = md.GetParameters();
                    //方法名相同且参数个数一样
                    if (mothodName == methodName && paramInfos.Length == paras.Length)
                    {
                        md.Invoke(obj, paras);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
     class ClassSample
    {
        public void test1(string para1)
        {
            Console.WriteLine("方式1 {0}________test111", para1);
        }

        public void test1(string para1, string para2)
        {
            Console.WriteLine("方式2 {0}________test111________{1}", para1, para2);
        }

        public void test2(string para1, string para2)
        {
            Console.WriteLine("方式3 {0}________test222________{1}", para1, para2);
        }
    }
}