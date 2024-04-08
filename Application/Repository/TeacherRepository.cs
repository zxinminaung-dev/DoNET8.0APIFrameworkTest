using ChatApp.Web.DataAccess;
using ChatApp.Web.Model.Entity;
using ChatApp.Web.Model.Repository;

namespace ChatApp.Web.Application.Repository
{
    public class TeacherRepository : RepositoryBase<Teacher>, ITeacherRepository
    {
        public TeacherRepository(IDbContext context) : base(context)
        {
        }
    }
}
