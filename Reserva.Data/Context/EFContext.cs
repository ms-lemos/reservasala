using System;
using System.Data.Entity;
using System.Linq;
using Reserva.Domain.Entities;

namespace Reserva.Data.Context
{
    public class EFContext : DbContext
    {
        public EFContext()
            : base("LocalDB")
        {
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("DtInclusao") != null))
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property("DtInclusao").CurrentValue = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Property("DtInclusao").IsModified = false;

                        //if (entry.Property("DtEdicao") != null)
                        //    entry.Property("DtEdicao").CurrentValue = DateTime.Now;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            return base.SaveChanges();
        }
        
        public DbSet<Usuario> Clientes { get; set; }
    }
}