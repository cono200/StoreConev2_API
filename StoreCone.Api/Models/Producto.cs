using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StoreCone.Api.Models; // Esto es para tener para poder usar la clase Proveedor

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
    [BsonElement("ProveedorId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProveedorId { get; set; } = string.Empty;
    [BsonElement("Descripcion")]
    public string Descripcion { get; set; } = string.Empty;
    [BsonElement("Precio")]

    public int Precio { get; set; }
    [BsonElement("Caducidad")]
    public DateTime Caducidad { get; set; } 

    [BsonIgnore] // Ignora este campo al guardar en la base de datos
    public Proveedor Proveedor { get; set; } // Agrega una propiedad Proveedor de tipo Proveedor
}
