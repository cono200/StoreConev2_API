using Drivers.Api.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StoreCone.Api.Models
{
    public class HistorialModel
    {
        
    [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Fecha")]
        public DateTime Fecha { get; set; }

        [BsonElement("Accion")]
        public string Accion { get; set; } = string.Empty;

        [BsonElement("Producto")]
        public string Producto { get; set; } = string.Empty;

        [BsonElement("ProductoId")]
        public string ProductoId { get; set; } = string.Empty;

        public Producto Detalles { get; set; }
    }
}

