using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Csharp.helpers
{
    public class SomeHelper
    {
        ///  <summary>  
        ///  序列化为二进制字节数组  
        ///  </summary>  
        ///  <param  name="request">要序列化的对象 model需加Serialize特性</param>  
        ///  <returns>字节数组 </returns>  
        public static byte[] SerializeBinary<T>(T request)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            serializer.Serialize(memStream, request);
            return memStream.GetBuffer();
        }
        ///  <summary>  
        ///  从二进制数组反序列化得到对象  
        ///  </summary>  
        ///  <param  name="buf">字节数组 </param>  
        ///  <returns>得到的对象 </returns>  
        public static T DeserializeBinary<T>(byte[] buf)
        {
            System.IO.MemoryStream memStream = new MemoryStream(buf);
            memStream.Position = 0;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter deserializer =
                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            T newobj = (T)deserializer.Deserialize(memStream);
            memStream.Close();
            return newobj;
        }
         /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"test"})</param>
        /// <returns>对象实体</returns>
        public T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"test"}])</param>
        /// <returns>对象实体集合</returns>
        public List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }
    }
}