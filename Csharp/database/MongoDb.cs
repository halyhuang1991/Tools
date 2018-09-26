using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Csharp
{
    public class MongoDb
    {
          private MongoDatabase repository;
          public MongoDb(string[] dic,string DatabaseName){
            //在mongodb shell 中使用use DatabaseName  db.collectionname.find()
            List<MongoServerAddress> servers = new List<MongoServerAddress>();
            foreach (var d in dic)
            {
                string host=d.Split(':')[0];
                int port=Convert.ToInt32(d.Split(':')[1]);
                MongoServerAddress sa = new MongoServerAddress(host, port);
                servers.Add(sa);
            }
           
            try{
                MongoClientSettings set = new MongoClientSettings();
                
                set.Servers = servers;
                set.MaxConnectionPoolSize = 2000;//设置连接池最大连接数
                if(dic.Length>1){
                       set.ReplicaSetName = "testrs";//设置副本集名称
                }
                set.ConnectTimeout = new TimeSpan(0, 0, 0, 10, 0);
                set.ReadPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
                // MongoCredential credential = MongoCredential.CreateCredential(DatabaseName,"username", "password");
                // set.Credential=credential;
                //Console.WriteLine(set.ToString());
                MongoClient client = new MongoClient(set);
               // MongoServer server = client.GetServer();
                this.repository = client.GetServer().GetDatabase(DatabaseName);
               // MongoServer server =  new MongoServer(MongoServerSettings.FromClientSettings(client.Settings));
                

            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
           
          }
          public void Insert<T>(T tClass)where T:class
          {
              MongoCollection col = this.repository.GetCollection("Users");//获得Users集合,如果数据库中没有，先新建一个 
              col.Insert<T>(tClass);  //执行插入操作 
          }
          public void Update(){
              var query = new QueryDocument { { "Name", "haly" } };
              var update = new UpdateDocument { { "$set", new QueryDocument { { "Sex", "man" } } } };
              MongoCollection col = this.repository.GetCollection("Users");
              col.Update(query, update);
          }
          public void Delete(){
               var query = new QueryDocument { { "Name", "haly" } };
                MongoCollection col = this.repository.GetCollection("Users");
              col.Remove(query);
          }
          public List<T> Query<T>()where T : class{
              var query = new QueryDocument { { "Name", "haly" } };
              return GetAll<T>("Users",query);

          }
          public  T GetOne<T>(string collectionName, IMongoQuery query) where T : class
        {
            T result = default(T);

            MongoCollection<T> categories = this.repository.GetCollection<T>(collectionName);
            result = categories.FindOneAs<T>(query);
            return result;
        }
        public  List<T> GetAll<T>(string collectionName, IMongoQuery query) where T : class
        {
            List<T> result = new List<T>();
            MongoCollection<T> categories = this.repository.GetCollection<T>(collectionName);
            foreach (T entity in categories.FindAll())
            {
                result.Add(entity);
            }
            return result;
        }
        public void InserBatch(){
            MongoCollection col = this.repository.GetCollection("Users");
            BsonDocument[] batch = {
                new BsonDocument {
                    { "author", "Kurt Vonnegut" },
                    { "title", "Cat's Cradle" }
                },
                new BsonDocument {
                    { "author", "Kurt Vonnegut" },
                    { "title", "Slaughterhouse-Five" }
                }
            };
            col.InsertBatch(batch);
        }
        public void Save(){
            MongoCollection books = this.repository.GetCollection("Users");
            var query = new QueryDocument {
                { "author", "Kurt Vonnegut" },
                { "title", "Cats Craddle" }
            };
            BsonDocument book = books.FindOneAs<BsonDocument>(query);
            Console.WriteLine(book.ToString());
            if (book != null)
            {
                book["title"] = "Cat's Cradle";
                books.Save(book);
            }
            var update = new UpdateDocument {
                { "$set", new BsonDocument("title", "Cat's Cradle") }
            };
            var updatedBook = books.Update(query, update);
        }
          
    }
}