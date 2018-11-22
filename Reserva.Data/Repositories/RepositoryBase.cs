using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Reserva.Domain.Entities;
using Reserva.Domain.Interfaces.Data;
using Reserva.Domain.Interfaces.Data.Repositories;
using Reserva.Domain.Validation;
using Reserva.Infra.Extensions;

namespace Reserva.Data.Repositories
{
    public class RepositoryBase<TEntity, TIdType> : IRepository<TEntity, TIdType>
        where TEntity : EntityBase<TIdType>
    {
        protected readonly IDbContext<TEntity> Context;
        private bool? _saveChanges;

        private bool? _throwExceptionIfInvalid;

        public RepositoryBase(IDbContext<TEntity> context)
        {
            Context = context;
        }

        protected virtual bool ThrowExceptionIfInvalid
        {
            get => _throwExceptionIfInvalid.GetValueOrDefault(true);
            set => _throwExceptionIfInvalid = value;
        }

        public bool IsSaveChanges
        {
            get => _saveChanges.GetValueOrDefault(true);
            set => _saveChanges = value;
        }

        protected void ProcessaLote(Acao action, List<TEntity> entities, int? paginacao = null)
        {
            for (int i = 0, j = 0; i < entities.Count; i++, j++)
            {
                var entity = entities[i];

                IsSaveChanges = j == paginacao;

                action(entity);

                if (IsSaveChanges)
                    j = 0;
            }

            SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) Context?.Dispose();
        }

        protected delegate void Acao(TEntity a);

        #region Public Methods

        public virtual TEntity GetById(TIdType id, bool nolock = false)
        {
            return Context.GetById(id, nolock);
        }

        public virtual void Insert(TEntity entity)
        {
            if (ThrowExceptionIfInvalid)
                entity.ThrowExcpetionIfInsertInvalid();

            Context.Insert(entity);

            if (IsSaveChanges)
                SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            if (ThrowExceptionIfInvalid)
                entity.ThrowExcpetionIfUpdateInvalid();

            var current = Context.GetById(entity.Codigo, true);

            if (current == null)
                throw GenericExceptions.NaoEncontrado;

            // Atualiza os dados que podem ser alterados
            current.UpdateAllProperties(entity);

            // Atualiza o objeto, incluindo os dados que podem estar faltando
            entity.UpdateAllProperties(current, false);

            Context.Update(current);

            if (IsSaveChanges)
                SaveChanges();
        }

        public virtual void InsertOrUpdate(TEntity entity)
        {
            if (GetById(entity.Codigo, true) == null)
                Insert(entity);
            else
                Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            var ent = GetById(entity.Codigo);

            if (ent == null)
                throw GenericExceptions.NaoEncontrado;

            Context.Remove(ent);

            if (IsSaveChanges)
                SaveChanges();
        }

        public void InsertOrUpdate(List<TEntity> entities, int? paginacao = null)
        {
            ProcessaLote(InsertOrUpdate, entities, paginacao);
        }

        public void Insert(List<TEntity> entities, int? paginacao = null)
        {
            ProcessaLote(Insert, entities, paginacao);
        }

        public void Update(List<TEntity> entities, int? paginacao = null)
        {
            ProcessaLote(Update, entities, paginacao);
        }

        public void Remove(List<TEntity> entities, int? paginacao = null)
        {
            ProcessaLote(Remove, entities, paginacao);
        }

        public IEnumerable<TQuery> SqlQuery<TQuery>(string sqlQuery, params object[] parameters) where TQuery : class
        {
            return Context.SqlQuery<TQuery>(sqlQuery, parameters);
        }

        public int SqlCommand(string sqlCommand, params object[] parameters)
        {
            return Context.SqlCommand(sqlCommand, parameters);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter, bool nolock = false)
        {
            return Context.SingleOrDefault(filter, nolock);
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, bool nolock = false)
        {
            return Context.Query(filter, nolock);
        }

        public virtual int Count()
        {
            return Context.Count();
        }

        public virtual bool HasAny(Expression<Func<TEntity, bool>> filter, bool nolock = false)
        {
            return Context.HasAny(filter, nolock);
        }

        public virtual IEnumerable<TEntity> SqlQuery(string sqlQuery, params object[] parameters)
        {
            return Context.SqlQuery<TEntity>(sqlQuery, parameters).ToList();
        }

        public virtual int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}