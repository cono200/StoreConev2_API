
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using StoreCone.Api.Configuration;
using StoreCone.Api.Models;

namespace StoreCone.Api.Services;

public class ProveedorServices
{
    private readonly IMongoCollection<Proveedor> _proveedorCollection;

    public ProveedorServices(
        IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _proveedorCollection =
            mongoDB.GetCollection<Proveedor>
            (databaseSettings.Value.CollectionName);
        }
    

    public async Task<List<Proveedor>>GetAsync()=>
    await _proveedorCollection.Find(_=> true).ToListAsync();

    public async Task<Proveedor> GetProveedorId(string id)
    {
        return await _proveedorCollection.FindAsync(new BsonDocument
        {{"_id", new ObjectId(id)}}).Result.FirstAsync();
    }

    public async Task InsertProveedor(Proveedor proveedor)
    {
        await _proveedorCollection.InsertOneAsync(proveedor);
    }

    public async Task UpdateProveedor(Proveedor proveedor)
    {
        var filter = Builders<Proveedor>.Filter.Eq(s=>s.Id, proveedor.Id);
        await _proveedorCollection.ReplaceOneAsync(filter, proveedor);
    }

    public async Task DeleteProveedor(string id)
    {
        var filter = Builders<Proveedor>.Filter.Eq(s=>s.Id, id);
        await _proveedorCollection.DeleteOneAsync(filter);
    }





}