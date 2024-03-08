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
        try
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
        catch (Exception ex)
        {
            throw new Exception("Error al obtener los productos de la base de datos", ex);
        }
    }


    public async Task InsertarProducto(Producto producto)
    {
        try
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser null");
            }
            await _productoCollection.InsertOneAsync(producto);

            var historialProducto = new HistorialModel
            {
                ProductoId = producto.Id,
                Fecha = DateTime.Now,
                Accion = "Registro",
                Producto = producto.Nombre
            };

            await _historialServices.InsertarHistorialProducto(historialProducto);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al insertar el producto en la base de datos", ex);
        }
    }

    public async Task BorrarProducto(string Id)
    {
        try
        {
            var producto = await ProductoPorId(Id);
            var filter = Builders<Producto>.Filter.Eq(s => s.Id, Id);
            var deleteResult=await
            _productoCollection.DeleteOneAsync(filter);

            if (deleteResult.DeletedCount == 0)
            {
                throw new Exception($"No se pudo borrar el producto con el ID: {Id}");
            }
            var historialProducto = new HistorialModel
            {
                ProductoId = Id,
                Fecha = DateTime.Now,
                Accion = "Eliminación",
                Producto = producto.Nombre
            };

            await _historialServices.InsertarHistorialProducto(historialProducto);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al borrar el producto de la base de datos", ex);
        }
    }

    public async Task EditarProducto(Producto producto)
    {
        try
        {
            var filter = Builders<Producto>.Filter.Eq(s => s.Id, producto.Id);
            await _productoCollection.ReplaceOneAsync(filter, producto);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al editar el producto en la base de datos", ex);
        }
    }


    public async Task<Producto> ProductoPorId(string Id)
    {
        try
        {
            var producto = await _productoCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
            if (producto == null)
            {
                throw new Exception($"No se encontró ningún producto con el ID: {Id}");
            }

            var proveedor = await _proveedorCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(producto.ProveedorId) } }).Result.FirstAsync();
          

            producto.Proveedor = proveedor;
            return producto;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener el producto o el proveedor de la base de datos", ex);
        }
    }

    public async Task<Producto> ProductoPorCodigo(int codigo)
    {
        try
        {
            var filter = Builders<Producto>.Filter.Eq(p => p.Codigo, codigo);
            var producto = await _productoCollection.Find(filter).FirstOrDefaultAsync();

            if (producto == null)
            {
                throw new Exception($"No se encontró ningún producto con el código: {codigo}");
            }

            var proveedorFilter = Builders<Proveedor>.Filter.Eq(p => p.Id, producto.ProveedorId);
            var proveedor = await _proveedorCollection.Find(proveedorFilter).FirstOrDefaultAsync();

            producto.Proveedor = proveedor;
            return producto;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener el producto o el proveedor de la base de datos", ex);
        }
    }


}
