using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StoreCone.Api.Models
{
    public class MermasModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; } = string.Empty;

        [BsonElement("Codigo")]

        public string Codigo { get; set; } = string.Empty;
        [BsonElement("Tipo_de_merma")]

        public string Tipo_de_merma { get; set; } =string.Empty;
        [BsonElement("Fecha_ingreso")]

        public DateTime Fecha_ingreso { get; set; }
        [BsonElement("Nombre_producto")]

        public string Nombre_producto { get; set; } = string.Empty;
    }
}
