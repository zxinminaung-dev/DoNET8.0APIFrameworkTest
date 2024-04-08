using ChatApp.Web.Controllers.Common;
using ChatApp.Web.Infrastructure;
using ChatApp.Web.Infrastructure.Common;
using ChatApp.Web.Mappers;
using ChatApp.Web.Model;
using ChatApp.Web.Model.Repository;
using ChatApp.Web.Model.Service;
using ChatApp.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController  : BaseController
    {
        IStudentRepository _studentRepo;
        IStudentService _studentSrv;
        StudentMapper mapper;
        public StudentController(IStudentRepository studentRepo, IStudentService studentService)
        {
            _studentRepo = studentRepo;
            _studentSrv = studentService;
            mapper = new StudentMapper();
        }
        [HttpGet]
        public JsonResult Get()
        {
            PageResult<Student> list = GetAllData();
            return Json(list);
        }
        public PageResult<Student> GetAllData()
        
        {
            QueryOptions<Student> queryoption = GetQueryOptions<Student>();
            StudentViewModel vm = GetRequestParameters();
            queryoption = mapper.PrepareQueryOptionForRepository(queryoption, vm);
            PageResult<Student> list = _studentRepo.GetPagedResults(queryoption);            
            return list;
        }
        public StudentViewModel GetRequestParameters()
        {
            StudentViewModel vm = new StudentViewModel();
            vm.name = Request.Query["name"].ToString();
            return vm;
        }
        [HttpPost]
        public JsonResult Save(Student student)
        {
            CommandResult<Student> result = new CommandResult<Student>();
            result = _studentSrv.SaveOrUpdate(student);
            return Json(result);
        }
        [HttpGet]
        [Route("getbyid")]
        public JsonResult GetbyId(int id) 
        {
            Student? student = _studentRepo.Get(id);
            return Json(student);
        }        
    }
}
