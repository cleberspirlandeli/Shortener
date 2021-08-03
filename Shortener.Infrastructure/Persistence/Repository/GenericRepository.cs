using MongoDB.Bson;
using MongoDB.Driver;
using Shortener.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortener.Infrastructure.Persistence.Repository
{
    public abstract class GenericRepository<TEntity> where TEntity : Entity
    {
        private protected readonly IMongoCollection<TEntity> _dbSet;

        public GenericRepository(IUrlShortenMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _dbSet = database.GetCollection<TEntity>(settings.CollectionName);
        }

        public async Task<List<TEntity>> GetAll() =>
            await _dbSet.Find(db => true).ToListAsync();

        public async Task<TEntity> GetById(string id) =>
           await _dbSet.Find<TEntity>(db => db.Id == id).FirstOrDefaultAsync();

        public TEntity Create(TEntity db)
        {
            _dbSet.InsertOne(db);
            return db;
        }

        public void Update(string id, TEntity dbIn) =>
            _dbSet.ReplaceOne(db => db.Id == id, dbIn);

        public void Remove(TEntity dbIn) =>
            _dbSet.DeleteOne(db => db.Id == dbIn.Id);

        public void Remove(string id) =>
            _dbSet.DeleteOne(db => db.Id == id);
    }
}

