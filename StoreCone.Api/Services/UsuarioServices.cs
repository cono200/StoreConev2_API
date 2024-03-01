using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using StoreCone.Api.Configuration;
using StoreCone.Api.Models;


namespace StoreCone.Api.Services
{
    public class UsuarioServices
    {
        private readonly IMongoCollection<UsuarioModel> _usuarioCollection;

        public UsuarioServices(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _usuarioCollection =
            mongoDB.GetCollection<UsuarioModel>
            (databaseSettings.Value.UsuarioCollectionName);
        }


        public async Task<List<UsuarioModel>> GetAsync() =>
        await _usuarioCollection.Find(_ => true).ToListAsync();

        public async Task<UsuarioModel> GetUsuarioId(string id)
        {
            return await _usuarioCollection.FindAsync(new BsonDocument
        {{"_id", new ObjectId(id)}}).Result.FirstAsync();
        }

        public async Task InsertUsuario(UsuarioModel usuario)
        {
            await _usuarioCollection.InsertOneAsync(usuario);
        }

        public async Task UpdateUsuario(UsuarioModel usuario)
        {
            var filter = Builders<UsuarioModel>.Filter.Eq(s => s.Id, usuario.Id);
            await _usuarioCollection.ReplaceOneAsync(filter, usuario);
        }

        public async Task DeleteUsuario(string id)
        {
            var filter = Builders<UsuarioModel>.Filter.Eq(s => s.Id, id);
            await _usuarioCollection.DeleteOneAsync(filter);
        }


    }
}
