using ChatApp.Web.Infrastructure.Common;
using ChatApp.Web.Model;
using ChatApp.Web.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Web.Controllers.Common
{
    public abstract class BaseController : Controller
    {
        public QueryOptions<TEntity> GetQueryOptions<TEntity>() where TEntity:BaseEntity
        {
            QueryOptions<TEntity> queryOptions = new QueryOptions<TEntity>();
            string page = Request.Query["page"].ToString();
            if (!string.IsNullOrEmpty(page))
            {
                queryOptions.Page = Convert.ToInt32(page);
            }
            string record = Request.Query["record"].ToString();
            if (!string.IsNullOrEmpty(record))
            {
                queryOptions.RecordPerPage = Convert.ToInt32(record);
            }
            var SortColumn = Request.Query["sortBy"].FirstOrDefault();
            if (!string.IsNullOrEmpty(SortColumn))
            {
                queryOptions.SortColumnName = SortColumn;
            }
            var SortColumnDirection = Request.Query["sortOrder"].FirstOrDefault();
            if (!string.IsNullOrEmpty(SortColumnDirection))
            {
                if (SortColumnDirection == "desc" || SortColumnDirection == "DESC")
                {
                    queryOptions.SortOrder = SortOrder.DESC;
                }
                else
                {
                    queryOptions.SortOrder = SortOrder.ASC;
                }
            }
            else
            {
                queryOptions.SortOrder = SortOrder.DESC;
            }
            return queryOptions;
        }
    }
}
