using System;
using System.Linq;
using System.Linq.Expressions;

namespace Reserva.Domain.Interfaces.Data
{
    public interface IDbContext<TEntity>
    {
        TEntity GetById<TIdType>(TIdType id, bool nolock = false);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        IQueryable<TEntity> SqlQuery(string sqlQuery, params object[] parameters);
        IQueryable<TQuery> SqlQuery<TQuery>(string sqlQuery, params object[] parameters) where TQuery : class;

        int SqlCommand(string sqlCommand, params object[] parameters);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter, bool nolock = false);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, bool nolock = false);

        bool HasAny(Expression<Func<TEntity, bool>> filter, bool nolock = false);

        int Count();

        int SaveChanges();

        void Dispose();
    }
}