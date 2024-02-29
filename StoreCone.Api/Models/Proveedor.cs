using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreCone.Api.Models;
//SHIFT + ALT  + F
public class Proveedor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; } = string.Empty;

    [BsonElement("Nombre")]

    public string Nombre { get; set; } = string.Empty;
    [BsonElement("Telefono")]

    public int Telefono { get; set; }
    [BsonElement("Correo")]

    public string Correo { get; set; } = string.Empty;
    [BsonElement("Producto")]

    public string Producto { get; set; } = string.Empty;

}