using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ChatApp.Web.DataAccess
{
    public interface IDbContext :  IDisposable
    {
        public DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public DatabaseFacade databaseFacade();

        void SetAddedState<TEntity>(TEntity entity) where TEntity : class;
        void SetModifiedState<TEntity>(TEntity entity) where TEntity: class;
        void SetDeletedState<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
