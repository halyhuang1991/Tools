using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using Csharp.Models;

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