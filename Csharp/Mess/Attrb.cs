using System;
using System.Linq;
using System.Reflection;

namespace Csharp.Mess
{
    [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
sealed class MyAttribute : System.Attribute
{
    // See the attribute guidelines at
    //  http://go.microsoft.com/fwlink/?LinkId=85236
    readonly string positionalString;
    
    // This is a positional argument
    public MyAttribute(string positionalString)
    {
        this.positionalString = positionalString;
        
        // TODO: Implement code here
        //throw new System.NotImplementedException();//查询特性会报错
    }
    
    public string PositionalString
    {
        get { return positionalString; }
    }
    
    // This is a named argument
    public int NamedInt { get; set; }
}

    [My("ok")]
    public class Attrb
    {
         [My("ok1111")]
        public string flag;
        [My("ok1")]
        public string CSTORD{get;set;}
         [My("ok2")]
        public static void log(string msg){
                 Console.WriteLine(msg);
                 
        }
        public void Test(){
            var tp=typeof(Attrb);
            Attribute[] abs=Attribute.GetCustomAttributes(tp);
            int i=abs.Length;
            MyAttribute ma=abs[0] as MyAttribute;
            Console.WriteLine(ma.PositionalString);
            //-----------------------------
            PropertyInfo property =typeof(Attrb).GetProperty("CSTORD");
            Attribute[] ab=Attribute.GetCustomAttributes(property);
             MyAttribute ma1=ab[0] as MyAttribute;
            Console.WriteLine("Attribute.GetCustomAttributes----"+ma1.PositionalString);
            //------------------------------
            Attrb atb=new Attrb();
            PropertyInfo pInstance = typeof(Attrb).GetProperty("CSTORD");
            pInstance.SetValue(atb, "12", null);//设置指定实例 属性 的值
            Console.WriteLine(atb.CSTORD);
            //----------------------
            Console.WriteLine(GetName());
             Console.WriteLine(GetName1());
             MethodInfo method = typeof(Attrb).GetMethod("log");
            var ob=method.GetCustomAttributes(typeof(MyAttribute)).FirstOrDefault();
             Console.WriteLine(((MyAttribute)ob).PositionalString);
            //-----------------------------------
            Console.WriteLine("------------GetProperties-----------");//get set 属性
            foreach (var p in this.GetType().GetProperties())
            {
                Console.WriteLine(p.Name);
            }
            Console.WriteLine("-------GetFields---------------");//字段
            foreach (var p in this.GetType().GetFields())
            {
                Console.WriteLine(p.Name);
            }


        }
        private static string GetName()
        {
            var type = typeof (Attrb);

            var fieldInfo = type.GetProperty("CSTORD");
            if (fieldInfo == null)
            {
                return null;
            }

            var attribute = fieldInfo.GetCustomAttributes(typeof(MyAttribute), false).FirstOrDefault();

            if (attribute == null)
            {
                return null;
            }

            return ((MyAttribute) attribute).PositionalString;
        }
         private static string GetName1()
        {
            var type = typeof (Attrb);

            var fieldInfo = type.GetField("flag");
            if (fieldInfo == null)
            {
                return null;
            }

            var attribute = fieldInfo.GetCustomAttributes(typeof(MyAttribute), false).FirstOrDefault();

            if (attribute == null)
            {
                return null;
            }

            return ((MyAttribute) attribute).PositionalString;
        }
    }
}