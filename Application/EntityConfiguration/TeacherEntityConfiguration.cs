using ChatApp.Web.DataAccess;
using ChatApp.Web.Model.Entity;
using ChatApp.Web.Model.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Web.Application.EntityConfiguration
{
    public class TeacherEntityConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
           
        }
    }
}
