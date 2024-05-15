using GlobalThings.Domain.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GlobalThings.Repository.Context
{
    public class DbContext
    {
        private readonly IMongoDatabase _mongoDb;
        public IMongoDatabase MongoDb => _mongoDb;
        private readonly MongoClient _client;
        public MongoClient Cliente => _client;

        public DbContext(IOptions<MonitorConfiguration> options)
        {
            _client = new MongoClient(options.Value.DatabaseSettings.ConnectionString);
            _mongoDb = _client.GetDatabase(options.Value.DatabaseSettings.DatabaseName);
        }
    }
}
