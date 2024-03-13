using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StoreCone.Api.Models
{
    public class Pistola
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        //[BsonElement("FechaCreacion")]
        //public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [BsonElement("Codigo")]
        public long CodigoPistola { get; set; }

    }
}
