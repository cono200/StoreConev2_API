using StoreCone.Api.Configuration;
using StoreCone.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace StoreCone.Api.Services;
public class HistorialServices
{
    private readonly IMongoCollection<HistorialModel> _historialProductoCollection;

    public HistorialServices(
        IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _historialProductoCollection = mongoDB.GetCollection<HistorialModel>(databaseSettings.Value.HistorialCollectionName);
    }

    public async Task InsertarHistorialProducto(HistorialModel historialProducto)
    {
        await _historialProductoCollection.InsertOneAsync(historialProducto);
    }

}
