using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Api.Socioboard.Model
{
    public class MongoRepository : IMongoRepository
    {
        //private IMongo _provider;
        private IMongoDatabase _db;

        private MongoCollectionSettings settings;
        private string collecionName;

        public MongoRepository(string CollectionName)
        {
            //var credential = MongoCredential.CreateMongoCRCredential("admin", "suresh", "BAsu_3542");

            //var clientsettings = new MongoClientSettings
            //{
            //    Credentials = new[] { credential }
            //};
            //MongoClient client = new MongoClient(clientsettings);

            MongoClient client = new MongoClient(System.Configuration.ConfigurationManager.AppSettings["MongoDbConnectionString"]);
            
            _db = client.GetDatabase(System.Configuration.ConfigurationManager.AppSettings["MongoDbName"]);


            //MongoClient client = new MongoClient();

            ////var server = client.GetServer();
            //_db = client.GetDatabase("Socioboard");
            this.collecionName = CollectionName;


            // this.settings.
        }

        //   public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        //where T : class, new()
        //   {
        //       var items = All<T>().Where(expression);
        //       foreach (T item in items)
        //       {
        //           Delete(item);
        //       }
        //   }

        public void Delete<T>(FilterDefinition<T> filter) where T : class, new()
        {
            _db.GetCollection<T>(collecionName, settings).DeleteOneAsync(filter);
        }

        public void DeleteAll<T>() where T : class, new()
        {
            _db.DropCollectionAsync(typeof(T).Name);
        }

        public async Task<IList<T>> Find<T>(Expression<Func<T, bool>> query) where T : class, new()
        {
            // Return the enumerable of the collection
            var collection = _db.GetCollection<T>(collecionName, settings).Find<T>(query);
            try
            {
                var output = await collection.ToListAsync().ConfigureAwait(false);
                return output;
            }
            catch (Exception e) 
            {
                return null;
            }
        }
        public async Task<IList<T>> FindWithRange<T>(Expression<Func<T, bool>> query, SortDefinition<T> sort, int skip, int take) where T : class, new()
        {
            var collection = _db.GetCollection<T>(collecionName, settings).Find<T>(query).Sort(sort).Limit(take).Skip(skip);
            try
            {
                var output = await collection.ToListAsync().ConfigureAwait(false);
                return output;
            }
            catch (Exception e)
            {
                return null;
            }
        }


     //   public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
     //where T : class, new()
     //   {
     //       return All<T>().Where(expression).SingleOrDefault();
     //   }

        //public IQueryable<T> All<T>() where T : class, new()
        //{
        //    return _db.GetCollection<T>(collecionName).FindAll<T>() ;
        //}

        //public IQueryable<T> All<T>(int page, int pageSize) where T : class, new()
        //{
        //    return PagingExtensions.Page(All<T>(), page, pageSize);
        //}

        public System.Threading.Tasks.Task Add<T>(T item) where T : class, new()
        {
            var document = BsonDocument.Parse(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(item));
            var collection = _db.GetCollection<BsonDocument>(collecionName);
            return collection.InsertOneAsync(document);
        }

        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        public System.Threading.Tasks.Task Update<T>(UpdateDefinition<BsonDocument> document, FilterDefinition<BsonDocument> filter) 
        {
            //var document = BsonDocument.Parse(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(item));
            var collection = _db.GetCollection<BsonDocument>(collecionName);
            return collection.UpdateOneAsync(filter, document);
        }

        public System.Threading.Tasks.Task AddList<T>(IEnumerable<T> items) where T : class, new()
        {
            List<BsonDocument> lstbson = new List<BsonDocument>();
            foreach (var item in items)
            {
                BsonDocument document = BsonDocument.Parse(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(item));
                lstbson.Add(document);
            }
            var collection = _db.GetCollection<BsonDocument>(collecionName);
            return collection.InsertManyAsync(lstbson);

        }
        public void Dispose()
        {
            // _db.Dispose();
        }
    }
}