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
    private readonly IMongoCollection<Proveedor> _proveedorCollection;
    private readonly HistorialServices _historialServices;

    public ProductoServices(
        IOptions<DatabaseSettings> databaseSettings,
        HistorialServices historialServices)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _productoCollection = mongoDB.GetCollection<Producto>(databaseSettings.Value.ProdCollectionName);
        _proveedorCollection = mongoDB.GetCollection<Proveedor>(databaseSettings.Value.CollectionName);
        _historialServices = historialServices;
    }

    public async Task<List<Producto>> GetAsync()
    {
        var productos = await _productoCollection.Find(_ => true).ToListAsync();
        foreach (var producto in productos)
        {
            var proveedores = await _proveedorCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(producto.ProveedorId) } }).Result.ToListAsync();
            if (proveedores.Any())
            {
                var proveedor = proveedores.First();
                producto.Proveedor = proveedor;
            }
        }
        return productos;
    }

    public async Task InsertarProducto(Producto producto)
    {
        await _productoCollection.InsertOneAsync(producto);

        var historialProducto = new HistorialModel
        {
            ProductoId = producto.Id,
            Fecha = DateTime.Now,
            Accion= "Registro",
            Producto = producto.Nombre

        };

        await _historialServices.InsertarHistorialProducto(historialProducto);
    }

    public async Task BorrarProducto(string Id)
    {
        var filter = Builders<Producto>.Filter.Eq(s => s.Id, Id);
        await _productoCollection.DeleteOneAsync(filter);
    }

    public async Task EditarProducto(Producto producto)
    {
        var filter = Builders<Producto>.Filter.Eq(s => s.Id, producto.Id);
        await _productoCollection.ReplaceOneAsync(filter, producto);
    }

    public async Task<Producto> ProductoPorId(string Id)
    {
        var producto = await _productoCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
        var proveedor = await _proveedorCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(producto.ProveedorId) } }).Result.FirstAsync();
        producto.Proveedor = proveedor;
        return producto;
    }
}
