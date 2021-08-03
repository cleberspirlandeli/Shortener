using MongoDB.Driver;
using Shortener.Domain;
using Shortener.Domain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Infrastructure.Persistence.Repository
{
    public class UrlRepository : GenericRepository<Url>
    {
        public UrlRepository(IUrlShortenMongoDbSettings settings) : base(settings) { }

        public async Task<Url> GetUrlByKey(string id) =>
           await _dbSet.Find(db => db.KeyUrl == id).FirstOrDefaultAsync();
    }
}
