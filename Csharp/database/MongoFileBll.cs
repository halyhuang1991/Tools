using System;
using System.Collections.Generic;
using System.IO;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Csharp
{
    public class MongoFileBll
    {
        private MongoDatabase repository;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MongoCursor<MongoGridFSFileInfo> FindAll()
        {
            return this.repository.GetGridFS(MongoGridFSSettings.Defaults).FindAll();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pConnectionstring"></param>
        
        public MongoFileBll(string pConnectionstring)
        {
            //Console.WriteLine("run1");
            //mongodb://username:password@myserver:port/databaseName
            //mongodb://username:password@ds011111.mongolab.com:11111/db-name
            MongoUrl mongourl = MongoUrl.Create(pConnectionstring);
            // var settings = MongoClientSettings.FromUrl(mongourl);
            // settings.Credentials = new List<MongoCredential>()
            // {
            //      MongoCredential.CreateCredential(mongourl.DatabaseName, "admin", "123456")
            // };
            MongoClient client = new MongoClient(mongourl);
            MongoServer server = client.GetServer();
            this.repository = server.GetDatabase(mongourl.DatabaseName);

       
             
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void UploadFile(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            this.repository.GetGridFS(MongoGridFSSettings.Defaults).Upload(filePath, fi.Name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>

        public void UploadFile(string filePath, string fileName)
        {
            this.repository.GetGridFS(MongoGridFSSettings.Defaults).Upload(filePath, fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        public void DownloadFile(string fileName)
        {
            this.repository.GetGridFS(MongoGridFSSettings.Defaults).Download(fileName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void DeleteFile(string fileName)
        {
            this.repository.GetGridFS(MongoGridFSSettings.Defaults).Delete(fileName);
        }
        /// <summary>
        /// 
        /// </summary>
        public void DeleteAll()
        {
            foreach (var inst in this.repository.GetGridFS(MongoGridFSSettings.Defaults).FindAll())
            {
                inst.Delete();
            }
        }
        

    }
}