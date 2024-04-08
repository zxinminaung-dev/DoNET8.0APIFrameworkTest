using ChatApp.Web.Model.Common;

namespace ChatApp.Web.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContext _dbContext;
        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
        //public int GetId<TEntity>(TEntity entity) where TEntity : BaseEntity
        //{
        //   int id = (int)entity.GetType().GetProperty("id").GetValue(entity);
        //    return id;
        //}
    }
}
