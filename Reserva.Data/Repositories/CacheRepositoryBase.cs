using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Caching;
using Reserva.Domain.Entities;
using Reserva.Domain.Interfaces.Data;

namespace Reserva.Data.Repositories
{
    public class CachedRepositoryBase<TEntity, TIdType> : RepositoryBase<TEntity, TIdType>
        where TEntity : EntityBase<TIdType>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly object CacheLockObject = new object();

        private readonly MemoryCache _cache;
        private readonly string _cacheKey;

        public CachedRepositoryBase(IDbContext<TEntity> context) : base(context)
        {
            _cache = MemoryCache.Default;
            _cacheKey = $"ReservaCachedRepository-{typeof(TEntity).Name}";
        }

        private IList<TEntity> ThreadSafeCacheAccessAction(Expression<Func<TEntity, bool>> filter = null,
            Action<IList<TEntity>> action = null, bool nolock = false)
        {
            IList<TEntity> all;

            lock (CacheLockObject)
            {
                all = _cache.Get(_cacheKey) as IList<TEntity>;

                if (all == null)
                {
                    all = Context.Query(nolock: nolock).ToList();
                    _cache.Add(_cacheKey, all, DateTimeOffset.Now.AddHours(2));
                }

                action?.Invoke(all);
            }

            return filter == null ? all : all.Where(filter.Compile()).ToList();
        }

        #region Public Methods

        public override TEntity GetById(TIdType id, bool nolock = false)
        {
            var entry = ThreadSafeCacheAccessAction(nolock: nolock, filter: e => e.Codigo.Equals(id)).FirstOrDefault();
            return entry;
        }

        public override void Insert(TEntity entity)
        {
            Context.Insert(entity);
            ThreadSafeCacheAccessAction(action: l => l.Add(entity));
        }

        public override void Update(TEntity entity)
        {
            Context.Update(entity);
            ThreadSafeCacheAccessAction(action: l =>
            {
                var atual = l.FirstOrDefault(e => e.Codigo.Equals(entity.Codigo));
                l.Remove(atual);
                l.Add(entity);
            });
        }

        public override void Remove(TEntity entity)
        {
            Context.Remove(entity);
            ThreadSafeCacheAccessAction(action: l =>
            {
                var atual = l.FirstOrDefault(e => e.Codigo.Equals(entity.Codigo));
                l.Remove(atual);
            });
        }

        public override IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, bool nolock = false)
        {
            return ThreadSafeCacheAccessAction(nolock: nolock, filter: filter).AsQueryable();
        }


        public override int Count()
        {
            return ThreadSafeCacheAccessAction().Count;
        }


        public override bool HasAny(Expression<Func<TEntity, bool>> filter, bool nolock = false)
        {
            return ThreadSafeCacheAccessAction().Any(filter.Compile());
        }

        #endregion
    }
}