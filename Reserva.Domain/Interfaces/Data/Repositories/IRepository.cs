using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Reserva.Domain.Entities;

namespace Reserva.Domain.Interfaces.Data.Repositories
{
    public interface IRepository<TEntity, TIdType> : IDisposable
        where TEntity : EntityBase<TIdType>
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void InsertOrUpdate(TEntity entity);
        void Remove(TEntity entity);

        void InsertOrUpdate(List<TEntity> entities, int? paginacao = null);
        void Insert(List<TEntity> entities, int? paginacao = null);
        void Update(List<TEntity> entities, int? paginacao = null);
        void Remove(List<TEntity> entities, int? paginacao = null);

        bool HasAny(Expression<Func<TEntity, bool>> filter, bool nolock = false);
        TEntity GetById(TIdType id, bool nolock = false);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter, bool nolock = false);
        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, bool nolock = false);

        int SqlCommand(string sqlCommand, params object[] parameters);

        IEnumerable<TEntity> SqlQuery(string sqlQuery, params object[] parameters);
        IEnumerable<TQuery> SqlQuery<TQuery>(string sqlQuery, params object[] parameters) where TQuery : class;

        int Count();
        int SaveChanges();
    }
}