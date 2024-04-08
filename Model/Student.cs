using ChatApp.Web.Model.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Web.Model
{
    [Table("Student")]
    public class Student : BaseEntity
    {
        [Required]
        public string name { get; set; }
    }
}
