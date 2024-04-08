using ChatApp.Web.Infrastructure.Common;
using ChatApp.Web.Model.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace ChatApp.Web.DataAccess
{
    public class ServiceBase<TEntity> : IService<TEntity> where TEntity : BaseEntity, new()
    {
        private IDbContext _context;
        private IUnitOfWork uom;
        public ServiceBase(IDbContext context , IUnitOfWork uom) 
        { 
            this._context = context;
            this.uom = uom;
        }
        public CommandResult<TEntity> Delete(TEntity entity)
        {
            CommandResult<TEntity> result = new CommandResult<TEntity>();
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SetDeletedState(entity);
                uom.Commit();
                result.success = true;
                result.id= (int)entity.GetType().GetProperty("id").GetValue(entity);
            }
            catch(Exception ex)
            {
                Int32 ErrorCode = ((SqlException)ex.InnerException).Number;
                result.success = false;
                /***To Check ForeignKey Constraint Error***/
                result.code = ErrorCode;
                result.messages.Add(ex.Message);
            }
            return result;
        }

        public CommandResult<TEntity> SaveOrUpdate(TEntity entity)
        {
            CommandResult<TEntity> result = new CommandResult<TEntity>();
            try
            {
                _context.Set<TEntity>().Add(entity);
                if (entity.id > 0)
                {
                    _context.SetModifiedState(entity);
                }
                else
                {
                    _context.SetAddedState(entity);
                }
                uom.Commit();
                result.success = true;
                result.id = (int)entity.GetType().GetProperty("id").GetValue(entity);
            }
            catch (Exception ex)
            {
                Int32 ErrorCode = ((SqlException)ex.InnerException).Number;
                result.success = false;
                /***To Check ForeignKey Constraint Error***/
                result.code = ErrorCode;
                result.messages.Add(ex.Message);
            }
            return result;
        }
        
    }
}
