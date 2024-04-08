using ChatApp.Web.Infrastructure;
using ChatApp.Web.Infrastructure.Common;
using ChatApp.Web.Model.Common;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Web.DataAccess
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly IDbContext _context;
        public RepositoryBase(IDbContext context) :base() 
        { 
            this._context = context;
        }

        public TEntity? Get(int id)
        {
            TEntity? entity = _context.Set<TEntity>().Where(x => x.id == id).FirstOrDefault();
            return entity;
        }

        public List<TEntity> Get()
        {
            List<TEntity> list = _context.Set<TEntity>().ToList();
            return list;
        }

        public PageResult<TEntity> GetPagedResults(QueryOptions<TEntity> option)
        {
            PageResult<TEntity> results = new PageResult<TEntity>();            
            if(option != null)
            {
                if (option.Page == 0)
                {
                    option.Page = 1;
                }
                int skip = (option.Page - 1 ) * option.RecordPerPage;
                if (option.FilterBy != null)
                {
                    results.total = _context.Set<TEntity>().Where(option.FilterBy).Count();
                }
                else
                {
                    results.total = _context.Set<TEntity>().Count();
                }
                if(results.total > 0)
                {
                    var query = _context.Set<TEntity>();

                    if (option.FilterBy != null)
                    {
                       // results.filterBy = option.FilterBy.GetType().GetMembers().ToString();
                        query.Where(option.FilterBy);   
                        if(option.SortOrder == SortOrder.ASC)
                        {
                            var q = query.Where(option.FilterBy).OrderBy(option.SortBy[0]);

                            if (option.SortBy.Count > 1)
                            {
                                for (int i = 1; i < option.SortBy.Count; i++)
                                {
                                    q = q.ThenBy(option.SortBy[i]);
                                }
                            }

                            results.data = q.Skip(skip).Take(option.RecordPerPage).ToList();
                        }
                        else
                        {
                            var q = query.Where(option.FilterBy).OrderByDescending(option.SortBy[0]);

                            if (option.SortBy.Count > 1)
                            {
                                for (int i = 1; i < option.SortBy.Count; i++)
                                {
                                    q = q.ThenByDescending(option.SortBy[i]);
                                }
                            }

                            results.data = q.Skip(skip).Take(option.RecordPerPage).ToList();
                        }
                    }
                    else
                    {
                        if (option.SortOrder == SortOrder.DESC)
                        {
                            var q = query.OrderByDescending(option.SortBy[0]);

                            if (option.SortBy.Count > 1)
                            {
                                for (int i = 1; i < option.SortBy.Count; i++)
                                {
                                    q = q.ThenByDescending(option.SortBy[i]);
                                }
                            }
                            results.data = q.Skip(skip).Take(option.RecordPerPage).ToList();
                        }
                        else
                        {
                            IOrderedEnumerable<TEntity> tmp = query.OrderBy(option.SortBy[0]);
                            if (option.SortBy.Count > 1)
                            {
                                for (int i = 1; i < option.SortBy.Count; i++)
                                {
                                    tmp = tmp.ThenBy(option.SortBy[i]);
                                }
                            }

                            results.data = tmp.Skip(skip).Take(option.RecordPerPage).ToList();
                        }
                    }
                }
            }
            return results;

        }

        public int RawSQL(string sql, object[] parameters)
        {
            return _context.databaseFacade().ExecuteSqlRaw(sql, parameters);
        }
    }
}
