using ChatApp.Web.Model.Common;

namespace ChatApp.Web.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
