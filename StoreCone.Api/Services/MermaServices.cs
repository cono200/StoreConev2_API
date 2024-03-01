using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using StoreCone.Api.Configuration;
using StoreCone.Api.Models;


namespace StoreCone.Api.Services
{
    public class MermaServices
    {
        private readonly IMongoCollection<MermasModel> _mermasCollection;

        public MermaServices(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _mermasCollection =
            mongoDB.GetCollection<MermasModel>
            (databaseSettings.Value.CollectionName);
        }

        public async Task<List<MermasModel>> GetAsync() =>
    await _mermasCollection.Find(_ => true).ToListAsync();

        public async Task<MermasModel> GetProveedorId(string id)
        {
            return await _mermasCollection.FindAsync(new BsonDocument
        {{"_id", new ObjectId(id)}}).Result.FirstAsync();
        }

        public async Task InsertMerma(MermasModel mermas)
        {
            mermas.Fecha_ingreso=DateTime.Now;
            await _mermasCollection.InsertOneAsync(mermas);
        }

        public async Task UpdateMerma(MermasModel mermas)
        {
            var filter = Builders<MermasModel>.Filter.Eq(s => s.Id, mermas.Id);
            await _mermasCollection.ReplaceOneAsync(filter, mermas);
        }

        public async Task DeleteMerma(string id)
        {
            var filter = Builders<MermasModel>.Filter.Eq(s => s.Id, id);
            await _mermasCollection.DeleteOneAsync(filter);
        }
    }
}
