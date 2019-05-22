using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Csharp.Mess
{
    public class DtAsnc
    {
        static DataTable dataTable=Init();
        private static DataTable Init(){
            dataTable=new DataTable();
            dataTable.Columns.Add("key",typeof(string));
            dataTable.Columns.Add("value",typeof(string));
            DataRow dataRow=dataTable.NewRow();
            dataRow["key"]="1"; dataRow["value"]="1";
            dataTable.Rows.Add(dataRow);
            return dataTable;
        }
        public static void ResetDt(){
            dataTable = Init();
        }
        public static string GetText(string key,string value){
            string ret="";
            DataRow[] dataRows;
            lock (dataTable.Columns.SyncRoot)
            {
                dataRows = dataTable.Select("key='" + key + "'");
            }
            if(dataRows.Length>0){
                ret=dataRows[0]["value"].ToString().Trim();
            }else{
                lock (dataTable)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["key"] = key; dataRow["value"] = value;
                    dataTable.Rows.Add(dataRow);
                }
                ret=value.Trim();
            }
            return ret;
        }
        public static void Test(){
            int location=0;
            for(int i=0;i<10000;i++){
                Task.Run(()=>{
                    Interlocked.Increment(ref location);
                    string value=GetText("key","value"+location);
                    Console.WriteLine(value+" "+location);
                });
                // Task.Run(()=>{
                //     ResetDt();
                // });
                Action<int> action=(ii)=>{
                    Task.Run(()=>{
                        string value=GetText("key","value"+ii);
                        Console.WriteLine(value+" "+ii);
                    });
                };
                action(i);
            }
            Console.Read();
        }
    }
}