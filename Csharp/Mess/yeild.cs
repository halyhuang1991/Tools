using System;
using System.Collections;

namespace Csharp.Mess
{
    
    public class yeild1
    {
        public static IEnumerable Power(int number, int exponent)
        {
            int counter = 0;
            int result = 1;
            while (counter++ < exponent)
            {
                Console.Write("\r\n result={0}  ", result);
                result = result * number;
                yield return result;
            }
        }
        public static void run()
        {
            // Display powers of 2 up to the exponent 8:
            foreach (int i in Power(2, 8))
            {
                Console.Write("\r\n {0} ok ", i);
            }
            foreach (string day in GetEnumerator())
            {
                System.Console.Write(day + "\r\n ");
            }
        }
        static string[] days = { "Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat" };

        public static IEnumerable GetEnumerator()
        {
            for (int i = 0; i < days.Length; i++)
            {
                yield return days[i];
            }
        }
        
    }
    
}