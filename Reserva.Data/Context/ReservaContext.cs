using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Reserva.Domain.Interfaces.Data;

namespace Reserva.Data.Context
{
	[UsedImplicitly]
	public class ReservaContext<TEntity> : IDbContext<TEntity>, IDisposable where TEntity : class
	{
		private readonly EFContext<TEntity> _db;

		public ReservaContext(string database)
		{
			_db = new EFContext<TEntity>(database);
		}
        
        public TEntity GetById<TIdType>(TIdType id, bool nolock = false) 
		{
			if (nolock)
				SetNolock();

			var dbEntity = _db.Set<TEntity>().Find(id);

			if (dbEntity == null)
				return null;

			_db.Entry(dbEntity).State = EntityState.Detached;
			return dbEntity;
		}

		public void Insert(TEntity entity) 
		{
			_db.Set<TEntity>().Add(entity);
			//Db.SaveChanges();
		}

		public void Update(TEntity entity) 
		{
			var dbEntry = _db.Entry(entity);
			dbEntry.State = EntityState.Modified;
			//Db.SaveChanges();
		}

		public void Remove(TEntity entity) 
		{
			var dbEntry = _db.Entry(entity);
			dbEntry.State = EntityState.Deleted;
			//Db.SaveChanges();
		}

		public IQueryable<TEntity> SqlQuery(string sqlQuery, params object[] parameters) 
		{
			return _db.Database.SqlQuery<TEntity>(sqlQuery, parameters).AsQueryable().AsNoTracking();
		}

	    /// <inheritdoc />
	    public IQueryable<TQuery> SqlQuery<TQuery>(string sqlQuery, params object[] parameters) where TQuery : class
	    {
	        return _db.Database.SqlQuery<TQuery>(sqlQuery, parameters).AsQueryable().AsNoTracking();
        }

	    public int SqlCommand(string sqlCommand, params object[] parameters)
		{
			return _db.Database.ExecuteSqlCommand(sqlCommand, parameters);
		}

		public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter, bool nolock = false)
			
		{
			if (nolock)
				SetNolock();

			return _db.Set<TEntity>().SingleOrDefault(filter);
		}

		public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, bool nolock = false)
			
		{
			if (nolock)
				SetNolock();

			if (filter != null)
				return _db.Set<TEntity>().Where(filter).AsNoTracking();

			return _db.Set<TEntity>().AsNoTracking();
		}

		public bool HasAny(Expression<Func<TEntity, bool>> filter, bool nolock = false) 
		{
			if (nolock)
				SetNolock();

			return _db.Set<TEntity>().Any(filter);
		}

		public int Count() 
		{
			return _db.Set<TEntity>().Count();
		}

		public int SaveChanges()
		{
			return _db.SaveChanges();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing) _db?.Dispose();
		}

		private void SetNolock()
		{
			const string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";

			_db.Database.ExecuteSqlCommand(sql);
		}
	}
}