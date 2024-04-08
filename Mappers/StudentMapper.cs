using ChatApp.Web.Infrastructure;
using ChatApp.Web.Infrastructure.Common;
using ChatApp.Web.Model;
using ChatApp.Web.ViewModel;

namespace ChatApp.Web.Mappers
{
    public class StudentMapper
    {
        public QueryOptions<Student> PrepareQueryOptionForRepository(QueryOptions<Student> queryOption,StudentViewModel vm)
        {

            if (!string.IsNullOrEmpty(vm.name))
            {
                queryOption.FilterBy = (x => x.name.Contains(vm.name));
            }

            if (queryOption.SortColumnName!=null)
            {
                queryOption.SortBy = new List<Func<Student, object>>();
                if (queryOption.SortColumnName == "name")
                {
                    queryOption.SortBy.Add((x => x.name));
                }
                else
                {
                    //queryOption.SortOrder = SortOrder.DESC;
                    queryOption.SortBy.Add((x => x.id));
                }
            }
            else
            {
                queryOption.SortBy.Add((x => x.id));
            }
            return queryOption;
        }
    }
}