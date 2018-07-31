using MongoDB.Bson.Serialization.Attributes;

namespace Csharp.Models
{
    [BsonIgnoreExtraElements]
    public class users
    {
         public string Name { get; set; }
        public string Sex { set; get; }
    }
}