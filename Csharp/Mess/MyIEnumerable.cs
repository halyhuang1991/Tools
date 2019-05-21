using System;
using System.Collections;
using System.Collections.Generic;

namespace Csharp.Mess
{
    class Enumeratable1<T> : IEnumerable<T> { 
        private T[] t; 
        public string this[int index]
       {
           get { return this[index]; }
           set { this[index] = value; }
       }
        public Enumeratable1(T[] a) {
             t = new T[a.Length];
            for (int i = 0; i < t.Length; i++) { t[i] = a[i]; } } 
       public IEnumerator<T> GetEnumerator() { 
           for (int i = 0; i <t.Length; i++)
                {
                    yield return t[i];
                }
           //return new Enumerator1<T>(t); 
        } 
        IEnumerator IEnumerable.GetEnumerator() { 
            for (int i = 0; i <t.Length; i++)
                {
                    yield return t[i];
                }
            //return new Enumerator1<T>(t); 
         } 
    }

    public class MyIEnumerable:IEnumerable
    {
        private string[] arr=new string[1024];
        private int len=0;
        public string this[int index]
       {
           get { return arr[index]; }
           set { arr[index] = value; }
       }
        public void Add(string s)
        {
            if (len==arr.Length)
            {
                string[] oldArr = arr;
                arr = new string[oldArr.Length*2];
                oldArr.CopyTo(arr, 0);
            }
            arr[len]=s;
            len++;
            Console.WriteLine("add "+s);
        }
        public IEnumerable<string> BottomToTop { 
            get { for (int i = 0; i < len; i++) { yield return arr[i]; } } 
        }
        public IEnumerable<string> TopToBottom { 
            get { for (int i = len-1; i >=0; i--) { yield return arr[i]; } } 
        }
        // IEnumerator IEnumerable.GetEnumerator()
        // {
        //     for (int i = 0; i < arr.Length; i++)
        //     {
        //         yield return arr[i];
        //     }
        // } 
        public string Current
        {
            get
            {
                if (len == -1)
                {
                    throw new InvalidOperationException();
                }
                return arr[len];
            }
        }
        public IEnumerator GetEnumerator()
        {
             try
            {
                for (int i = 0; i <len; i++)
                {
                    yield return arr[i];
                }
            }
            finally
            {
                Console.WriteLine("停止迭代！"); 
            }
            //throw new NotImplementedException();
        }
    }
}