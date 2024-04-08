using ChatApp.Web.Infrastructure.Common;
using ChatApp.Web.Infrastructure;
using ChatApp.Web.Model.Common;

namespace ChatApp.Web.DataAccess
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        TEntity? Get(int id);
        List<TEntity> Get();
        PageResult<TEntity> GetPagedResults(QueryOptions<TEntity> option);
    }
}
