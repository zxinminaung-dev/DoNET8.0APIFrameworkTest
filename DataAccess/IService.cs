using ChatApp.Web.Infrastructure.Common;
using ChatApp.Web.Model.Common;

namespace ChatApp.Web.DataAccess
{
    public interface IService<TEntity> where TEntity : BaseEntity, new()
    {
        CommandResult<TEntity> SaveOrUpdate(TEntity entity);
        CommandResult<TEntity> Delete(TEntity entity);
    }
}
