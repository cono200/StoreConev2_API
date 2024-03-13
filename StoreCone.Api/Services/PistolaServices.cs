using Drivers.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StoreCone.Api.Configuration;
using StoreCone.Api.Models;

namespace StoreCone.Api.Services
{
    public class PistolaServices
    {
        private readonly IMongoCollection<Pistola> _pistolaCollection;
        public PistolaServices(
           IOptions<DatabaseSettings> databaseSettings, HistorialServices historialServices)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _pistolaCollection =
            mongoDB.GetCollection<Pistola>
            (databaseSettings.Value.PistolaCollectionName); //AQUI SE PONE EL NOMBRE QUE PUSIMOS EN LA CARPETA DE CONFIGURATION (DATABASESETTINGS)
            
        }


        public async Task InsertarPistola(Pistola pistola)
        {
            try
            {
                if (pistola == null)
                {
                    throw new ArgumentNullException(nameof(pistola), "La pistola no puede ser null");
                }
                await _pistolaCollection.InsertOneAsync(pistola);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la pistola en la base de datos", ex);
            }
        }


        public async Task<Pistola> ObtenerUltimaPistolaRegistrada()
        {
            try
            {
                var filter = Builders<Pistola>.Filter.Empty;
                var sort = Builders<Pistola>.Sort.Descending("_id");
                var pistola = await _pistolaCollection.Find(filter).Sort(sort).Limit(1).FirstOrDefaultAsync();
                return pistola;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la última pistola registrada en la base de datos", ex);
            }
        }









    }
}
