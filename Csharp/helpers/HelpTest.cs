using System;
using System.Threading.Tasks;

namespace Csharp.helpers
{
    public class HelpTest
    {
        public static void TestLock1(){
            LocalLock localLock = new LocalLock();
            for (int i = 0; i < 10; i++)
            {
                Action<int> action = (ii) =>
                {
                    Task task = new Task(() =>
                    {
                        localLock.ExecuteWithLock("key", "value" + ii, TimeSpan.FromSeconds(10), (string x, string y) =>
                        {
                            Console.WriteLine("locallock  sucessful." + y);
                        }, () =>
                        {
                            Console.WriteLine("locallock failed.");
                        });
                    });
                    task.Start();
                };
                action(i);
            }
            Console.Read();
        }
    }
}