using StoreCone.Api.Configuration;
using StoreCone.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Drivers.Api.Models;

namespace StoreCone.Api.Services;
public class ProductoServices
{
    private readonly IMongoCollection<Producto> _productoCollection;
    public ProductoServices(
        IOptions<DatabaseSettings> databaseSettings)
    {
        //CLIENTE DE MONGO
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        //BASE DE DATOS DE MONGO DB
        var mongoDB =
        mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _productoCollection =
        mongoDB.GetCollection<Producto>
            (databaseSettings.Value.ProdCollectionName);
    }

    //LISTAR TODOS LOS REGISTROS
    public async Task<List<Producto>> GetAsync() =>
        await _productoCollection.Find(_ => true).ToListAsync();

    //INSERTAR UN REGISTRO
    public async Task InsertarProducto(Producto producto)
    {
        await _productoCollection.InsertOneAsync(producto);
    }

    //ELIMINAR UN REGISTRO
    public async Task BorrarProducto(string Id)
    {
        var filter = Builders<Producto>.Filter.Eq(s => s.Id, Id);
        await _productoCollection.DeleteOneAsync(filter);
    }

    //EDITAR
    public async Task EditarProducto(Producto producto)
    {
        var filter = Builders<Producto>.Filter.Eq(s => s.Id, producto.Id);
        await _productoCollection.ReplaceOneAsync(filter, producto);
    }

    //ENCONTRAR POR ID
    public async Task<Producto> ProductoPorId(string Id)
    {
        return await _productoCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
    }
}
