using System;

namespace Csharp.Mess
{
    public class ReviseSinglequote
    {
        public static void test(){
            string s="ekey='SAMSVXX' and CPBRND='VICTORIA'S SECRET' and cstord like '% s's%' or skey in ('a's','sds','d's')";
            s="userid='halyhuang'";
            int ix=System.Text.RegularExpressions.Regex.Matches(s.ToLower(), "[\']").Count;
            Console.WriteLine("single quotes count="+ix);
            int index = 0;
            ix = 0; 
            int ip = 0; int iq = 0;
            char[] crs = s.Trim().ToCharArray();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            while (index < crs.Length)
            {
                bool t = true;//不是值里面的单引号
                if (crs[index] == '\'' && index != crs.Length - 1)
                {
                    bool tl=true;bool tr=true;
                    string[] rightstr = new string[4] { " and", " or", ",", ")" };
                    string[] leftstr = new string[5] { "like ", ",", "(", "=",">" };
                    foreach (string str in rightstr)
                    {
                        ix = s.ToLower().IndexOf(str, index);
                        if (ix > 0)
                        {
                            string val=s.Substring(index+1,ix-index-1);
                            if(val.Trim()==""){
                                 tr = false;//值右边的单引号
                                 break;
                            }
                           
                        }
                       
                    }
                    if (tr)
                    {
                        foreach (string str in leftstr)
                        {

                            ip = s.ToLower().LastIndexOf(str, index);
                            if (ip > 0)
                            {
                                if (str == "like ")
                                {
                                    ip += 4;
                                }
                                if(index - ip - 1<=0){
                                    tl = false;//值左边的单引号
                                    break;
                                }
                                string val = s.Substring(ip + 1, index - ip - 1);
                                if (val.Trim() == "")
                                {
                                    tl = false;//值左边的单引号
                                    break;
                                }
                               
                            }


                        }

                    }
                    
                    if(!(!tr||!tl)){
                        t=false;
                    }

                }
                if (!t)
                {
                    sb.Append('\''); sb.Append('\'');
                }
                else
                {
                    sb.Append(crs[index]);
                }
                index = index + 1;
            }
            Console.WriteLine(sb.ToString());
            
        }
    }
}