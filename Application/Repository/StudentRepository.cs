using ChatApp.Web.DataAccess;
using ChatApp.Web.Model;
using ChatApp.Web.Model.Repository;

namespace ChatApp.Web.Application.Repository
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbContext context) : base(context)
        {
        }
    }
}
