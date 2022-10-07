using BidvestMobilitySurveyBackendServer.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace BidvestMobilitySurveyBackendServer.Database
{
    internal class BaseRepository<Entity> : IRepository<Entity> where Entity : new()
    {

        protected readonly IMongoCollection<Entity> _collection;

        public BaseRepository(RepositoryManager repositoryManager)
        {
            _collection = repositoryManager.GetCollection<Entity>();
        }

        public Task DeleteCollection()
            => _collection.Database.DropCollectionAsync(_collection.CollectionNamespace.CollectionName);

        public Task<IEnumerable<Entity>> Find(Expression<Func<Entity, bool>> predicate, int skip = 0, int limit = 10000)
        {
            var result = _collection.Find(predicate).Skip(skip).Limit(limit).ToEnumerable();
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Entity>> Find(IQueryBuilder<Entity> queryBuilder, int skip = 0, int limit = 10000)
        {
            BaseQueryBuilder<Entity> baseQueryBuilder = (BaseQueryBuilder<Entity>)queryBuilder;
            var result = _collection.Find(baseQueryBuilder.Filter).Skip(skip).Limit(limit).ToEnumerable();
            return Task.FromResult(result);
        }

        public async Task<Entity> FindById(string id)
        {
            var result = await _collection.FindAsync(new BsonDocument("_id", ObjectId.Parse(id)));
            return await result.FirstOrDefaultAsync();
        }

        public async Task<Entity> FindOne(IQueryBuilder<Entity> queryBuilder)
        {
            BaseQueryBuilder<Entity> baseQueryBuilder = (BaseQueryBuilder<Entity>)queryBuilder;
            var result = await _collection.Find(baseQueryBuilder.Filter).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Entity> FindOne(Expression<Func<Entity, bool>> predicate)
        {
            var result = await _collection.Find(predicate).FirstOrDefaultAsync();
            return result;
        }

        public Task Insert(Entity data)
        {
            return _collection.InsertOneAsync(data);
        }

        public Task Insert(IEnumerable<Entity> data)
        {
            return _collection.InsertManyAsync(data);
        }

        public async Task<Entity> Update(string id, Entity data)
        {
            var result = await _collection.FindOneAndReplaceAsync(new BsonDocument("_id", ObjectId.Parse(id)), data
                , new FindOneAndReplaceOptions<Entity, Entity>
                {
                    ReturnDocument = ReturnDocument.After
                });
            return result;
        }

        public async Task<(bool updated, long count)> Update(IEnumerable<KeyValuePair<IQueryBuilder<Entity>, IUpdateBuilder<Entity>>> updates)
        {
            var models = updates.Select(x =>
            {
                var (query, update) = x;
                var _q = (BaseQueryBuilder<Entity>)query;
                var _u = (BaseUpdateBuilder<Entity>)update;
                return new UpdateOneModel<Entity>(_q.Filter, _u.Update);
            });

            var result = await _collection.BulkWriteAsync(models, new BulkWriteOptions { IsOrdered = false });
            var updated = result.IsAcknowledged && result.IsModifiedCountAvailable;
            var count = result.ModifiedCount;
            return (updated, count);
        }

        public async Task<Entity> Upsert(string id, Entity data)
        {
            var result = await _collection.FindOneAndReplaceAsync(new BsonDocument("_id", ObjectId.Parse(id)), data
                , new FindOneAndReplaceOptions<Entity, Entity>
                {
                    ReturnDocument = ReturnDocument.After,
                    IsUpsert = true
                });
            return result;
        }

        public async Task<long> DeleteById(string id)
        {
            var result = await _collection.DeleteOneAsync(new BsonDocument("_id", ObjectId.Parse(id)));
            return result.DeletedCount;
        }
        public async Task<long> DeleteOne(Expression<Func<Entity, bool>> predicate)
        {
            var result = await _collection.DeleteOneAsync(predicate);
            return result.DeletedCount;
        }

        public async Task<long> DeleteOne(IQueryBuilder<Entity> queryBuilder)
        {
            BaseQueryBuilder<Entity> baseQueryBuilder = (BaseQueryBuilder<Entity>)queryBuilder;
            var result = await _collection.DeleteOneAsync(baseQueryBuilder.Filter);
            return result.DeletedCount;
        }

        public IQueryable<Entity> AsQueryable()
        {
            return _collection.AsQueryable();
        }

    }

}
