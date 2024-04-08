using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace ChatApp.Web.Infrastructure.Common
{
    public class QueryOptions<TEntity>
    {
        public int Page { get; set; } = 0;
        public int RecordPerPage { get; set; } = 10;
        public string? SearchValue { get; set; }
        public string? SortColumnName { get; set; }
        public QueryOptions()
        {
            SortOrder = SortOrder.ASC;
            SortBy = new List<Func<TEntity, object>>();           
        }

        public Expression<Func<TEntity, bool>>? FilterBy { get; set; }
        public List<Func<TEntity, object>> SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
