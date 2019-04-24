using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Csharp.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Csharp.Mess
{
    public class TestXML
    {
        public static void run()
        {
            List<book> ls=new List<book>();
            for(int i=0;i<100;i++){
                book b=new book();
                b.id=i;b.name="test";
                ls.Add(b);
            }
            WriteListXMl<book>(ls);

        }
        private static string GetCacheXML(string cacheKey){
              //1、获取内存缓存对象
            IMemoryCache memoryCache=new MemoryCache(new MemoryCacheOptions());
            string result;
            if (!memoryCache.TryGetValue(cacheKey, out result)){
                result = $"LineZero{DateTime.Now}";
                XDocument doc = new XDocument();
                doc = XDocument.Load("SystemInfo.xml");
                var classData = (from n in doc.Root.Elements("Class")
                                 where n.Attribute("name").Value == "Cache"
                                 select n).ToList();
                foreach (var item in classData.Elements("Item"))
                {
                   
                    string name=(string)item.Attribute("name").Value;
                    if(cacheKey!=name)continue;
                    string desc=(string)item.Attribute("desc").Value;
                    string value=(string)item.Value;
                    result=value;
                    break;
                }
                memoryCache.Remove(cacheKey);
                //缓存优先级 （程序压力大时，会根据优先级自动回收）
                memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.NeverRemove));
            }else{
                //缓存回调 10秒过期会回调
                memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10))
                    .RegisterPostEvictionCallback((key, value, reason, substate) =>
                    {
                        Console.WriteLine($"键{key}值{value}改变，因为{reason}");
                    }));
            }
            return result;
        }
        private static void WriteListXMl<T>(List<T> ls)where T:class{
            string path;
            XmlDocument xml=new XmlDocument();
            var xmldecl = xml.CreateXmlDeclaration("1.0","utf-8",null);
            xml.AppendChild(xmldecl);

            Type type=typeof(T);
            string typename=type.Name;
            path=@"D:\C\github\Tools\Csharp\txt\"+typename+".xml";
            if(File.Exists(path)){
                File.Delete(path);
            }
            var root=xml.CreateElement("root");
            xml.AppendChild(root);
            foreach (T t in ls)
            {
                var child=xml.CreateElement(typename);
                foreach (var p in type.GetProperties())
                {
                    var content=xml.CreateElement(p.Name);
                    content.InnerText=p.GetValue(t).ToString();
                    child.AppendChild(content);
                    Console.WriteLine(p.Name+"--"+p.GetValue(t));
                }
                root.AppendChild(child);
                Console.WriteLine("----------------------------------");
            }
            xml.Save(path);
        }
        private static void ReadXml(){
            string path=@"D:\C\github\Tools\Csharp\txt\dt.xml";
            XmlDocument xml=new XmlDocument();
            xml.Load(path);
            XmlNode root=xml.SelectSingleNode("/NewDataSet");
            if(root.HasChildNodes){
               XmlNodeList childlist=root.ChildNodes;
               XmlNodeList nodelist=xml.SelectNodes("/NewDataSet/Table1");
               Console.WriteLine(childlist.Count+"--"+nodelist.Count);
               XmlElement node=xml.CreateElement("News");
               node.SetAttribute("id","11111");
               node.InnerText="ok";
               root.AppendChild(node);
               string id=node.Attributes["id"].Value;
               string content=node.InnerText;
               Console.WriteLine(id,content);
            }
            xml.Save(path);
        }
        private static void WriteXMl(){

            DataTable dataTable=new DataTable();
            DataColumn a=new DataColumn();
            a.Caption="test1";
            a.ColumnName="a";
            a.DataType=typeof(string);
            dataTable.Columns.Add(a);
            DataColumn b=new DataColumn();
            b.Caption="test2";
            b.ColumnName="b";
            b.DataType=typeof(string);
            dataTable.Columns.Add(b);
            for (int i = 0; i < 100; i++)
            {
                DataRow dr=dataTable.NewRow();
                dr[a]="test";
                dr[b]="testb"+i;
                dataTable.Rows.Add(dr);
            }
            DataSet dataSet=new DataSet();
            dataSet.Tables.Add(dataTable);
            using (FileStream fileStream = new FileStream(@"D:\C\github\Tools\Csharp\txt\schema.xml", FileMode.OpenOrCreate))
            {
                dataSet.WriteXmlSchema(fileStream);
            }
            using (FileStream fileStream = new FileStream(@"D:\C\github\Tools\Csharp\txt\dt.xml", FileMode.OpenOrCreate))
            {
                dataSet.WriteXml(fileStream);
            }
        }
    }
}