using System.Linq.Expressions;

namespace BidvestMobilitySurveyBackendServer.Services
{
    public interface IUpdateBuilder<TEntity>
    {
        IUpdateBuilder<TEntity> Set<TField>(Expression<Func<TEntity, TField>> field, TField value);
        IUpdateBuilder<TEntity> AddToSet<TField>(Expression<Func<TEntity, IEnumerable<TField>>> field, TField value);
        IUpdateBuilder<TEntity> AddToSetEach<TField>(Expression<Func<TEntity, IEnumerable<TField>>> field, IEnumerable<TField> values);
    }
}
