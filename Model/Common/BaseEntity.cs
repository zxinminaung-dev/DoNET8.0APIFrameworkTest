using System.ComponentModel.DataAnnotations;

namespace ChatApp.Web.Model.Common
{
    public class BaseEntity
    {
        [Key]
        public int id { get; set; }
    }
}
