

using Microsoft.Extensions.Options;
using MongoDB.Driver;
 using MongoDB.Driver.Linq;

 
 
 namespace Makerswebapp.DataContext{

 public class MongoOdometerContext
    {
            private readonly IMongoDatabase _database = null;

            public MongoOdometerContext(IOptions<Settings> settings)
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                if (client != null)
                    _database = client.GetDatabase(settings.Value.Database);
            }
/* 
            public IMongoCollection<GpsOdometerModel> Odometer
            {
                get
                {
                    return _database.GetCollection<GpsOdometerModel>("GpsOdometerModel");
                }
            }

             public IMongoQueryable<GpsOdometerModel> OdometerQuery
            {
                get
                {
                    return _database.GetCollection<GpsOdometerModel>("GpsOdometerModel").AsQueryable();
                }
            }
             public IMongoCollection<Zero> dts
            {
                get
                {
                    return _database.GetCollection<Zero>("dts");
                }
            }
             public IMongoCollection<Zero> dtsQuery
            {
                get
                {
                    return _database.GetCollection<Zero>("dts");
                }
            }
*/
           
           //generic
            public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower() + "s");
        }
         public IMongoQueryable<TEntity> GetCollectionQuery<TEntity>()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower() + "s").AsQueryable();
        }

    }
 }
 