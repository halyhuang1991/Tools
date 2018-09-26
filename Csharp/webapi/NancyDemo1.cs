using Owin;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Host.HttpListener;
using Nancy;
using Nancy.Extensions;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Csharp.webapi
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();//Owin 这个报错了
        }
    }
    public class NancyDemo1
    {
        public static void run()
        {
            var url = "http://+:8087";//Microsoft.Owin.Hosting
            using (WebApp.Start<Startup1>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }
        
    }
    public class BaseOpenAPIModule : NancyModule
    {
        public BaseOpenAPIModule()
        {     
        }
        public BaseOpenAPIModule(string modulePath)
            : base(modulePath)
        {
            Before += TokenValidBefore;            
        }
        /// <summary>
        /// validate the parameters in before pipeline
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private Response TokenValidBefore(NancyContext context)
        {
        ////to bind the parameters of the route parameters
            //var para = this.Bind<UrlParaEntity>();
        ////if pass the validate return null
            //return !para.Validate() ? Response.AsText("I think you are a bad man!!") : null; 
            return null;       
        }
    }
    /// <summary>
   /// 
   /// 可以通过 Before 进行 安全的验证
   /// </summary>
   public class HomeModule : NancyModule
   {
       public HomeModule()
       {
           Get["/"] = (x,p) => GetString();
           Get["/login"] = (x,p)  => {  return "person name :" + Request.Query.name + " age : " + Request.Query.age; };
       }
       public async Task<dynamic> GetString()
        {
            return await Task.FromResult("Hello");
        }
   }
   public class UrlParaEntity
    {        
        public string Type { get; set; }
        public string PageIndex { get; set; }
        public string PageSize { get; set; }
        public string Sign { get; set; }
        /// <summary>
        /// the key
        /// </summary>
        const string encryptKey = "c1a2t3c4h5e6r.";
        /// <summary>
        /// validate the parameters
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {            
            return this.Sign == EncryptHelper.GetEncryptResult((Type + PageIndex + PageSize),encryptKey);            
        }               
    }
      public class EncryptHelper
    {
        /// <summary>
        /// HMACMD5 encrypt
        /// </summary>
        /// <param name="data">the date to encrypt</param>
        /// <param name="key">the key used in HMACMD5</param>
        /// <returns></returns>
        public static string GetEncryptResult(string data, string key)
        {
            HMACMD5 source = new HMACMD5(Encoding.UTF8.GetBytes(key));
            byte[] buff = source.ComputeHash(Encoding.UTF8.GetBytes(data));
            string result = string.Empty;
            for (int i = 0; i < buff.Length; i++)
            {
                result += buff[i].ToString("X2"); // hex format
            }
            return result;
        }
    }

}