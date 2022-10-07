using BidvestMobilitySurveyBackendServer.Services;
using MongoDB.Driver;

namespace BidvestMobilitySurveyBackendServer.Database;

internal class RepositoryManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly MongoDbContext mongoContext;

    public RepositoryManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        mongoContext = (MongoDbContext)serviceProvider.GetRequiredService<IDatabaseContext>();
    }

    public IRepository<T> GetRepository<T>() where T : new()
    {
        return _serviceProvider.GetRequiredService<IRepository<T>>();
    }

    public T GetService<T>()
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    public IMongoCollection<T> GetCollection<T>(string name = default)
    {
        return mongoContext.GetCollection<T>(name);
    }

}
