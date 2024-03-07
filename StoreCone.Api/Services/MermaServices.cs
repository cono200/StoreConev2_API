using Drivers.Api.Models;
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
        private readonly HistorialServices _historialServices;


        public MermaServices(
            IOptions<DatabaseSettings> databaseSettings, HistorialServices historialServices)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _mermasCollection =
            mongoDB.GetCollection<MermasModel>
            (databaseSettings.Value.MermaCollectioName); //AQUI SE PONE EL NOMBRE QUE PUSIMOS EN LA CARPETA DE CONFIGURATION (DATABASESETTINGS)
            _historialServices = historialServices;
        }

        public async Task<List<MermasModel>> GetAsync() =>
    await _mermasCollection.Find(_ => true).ToListAsync();

        public async Task<MermasModel> GetMermabyId(string id)
        {
            return await _mermasCollection.FindAsync(new BsonDocument
        {{"_id", new ObjectId(id)}}).Result.FirstAsync();
        }

        public async Task InsertMerma(MermasModel mermas)
        {
            mermas.Fecha_ingreso=DateTime.Now;
            await _mermasCollection.InsertOneAsync(mermas);
            var historialProducto = new HistorialModel
            {
                ProductoId = mermas.Id,
                Fecha = DateTime.Now,
                Accion = "Mermas",
                Producto = mermas.Nombre_producto

            };

            await _historialServices.InsertarHistorialProducto(historialProducto);
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
