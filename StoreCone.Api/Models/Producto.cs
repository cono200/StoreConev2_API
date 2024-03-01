using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Drivers.Api.Models;

public class Producto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("Nombre")]
    public string Nombre { get; set; } = string.Empty;
    [BsonElement("Codigo")]
    public int Codigo { get; set; }
    [BsonElement("Seccion")]
    public string Seccion { get; set; } = string.Empty;
    [BsonElement("Proveedor")]
    public string Proveedor { get; set; } = string.Empty;
    [BsonElement("Descripcion")]
    public string Descripcion { get; set; } = string.Empty;
    [BsonElement("Precio")]
    public int Precio { get; set; }
}