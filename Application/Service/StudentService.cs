using ChatApp.Web.DataAccess;
using ChatApp.Web.Model;
using ChatApp.Web.Model.Service;

namespace ChatApp.Web.Application.Service
{
    public class StudentService : ServiceBase<Student>, IStudentService
    {
        public StudentService(IDbContext context, IUnitOfWork uom) 
            : base(context, uom)
        {
        }
    }
}
