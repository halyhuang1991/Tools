using System;
using System.Linq;

namespace Csharp.Mess
{
    public class MessTest
    {
        public static void testEnumable(){
            MyIEnumerable a = new MyIEnumerable() { "张三", "李四" };
            a.Add("ok");
            foreach(string s in a.TopToBottom){
                Console.WriteLine("foreach "+s);
            }
            foreach (string s in a)
            {
                Console.WriteLine("foreach2 " + s);
            }
            Enumeratable1<string> ls=new Enumeratable1<string>(new string[]{"aaaa","b"});
            var p=from q in ls.AsEnumerable() where q.Length>1 select q;
            foreach(string s in p){
                Console.WriteLine("foreach3 " + s);
            }
            Console.WriteLine("index==="+a[1]);
        }
    }
}