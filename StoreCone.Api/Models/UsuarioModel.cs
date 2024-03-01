using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StoreCone.Api.Models
{
    public class UsuarioModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;

    }
}
