namespace ChatApp.Web.Infrastructure.Common
{
    public class CommandResult<TEntity>
    {
        public int? id { get; set; } = 0;
        public int code { get; set; } = 0;
        public bool success { get; set; } = false;
        public List<TEntity> result { get; set;}
        public List<string> messages { get; set;}
        public CommandResult()
        {
            result = new List<TEntity>();
            messages = new List<string>();
        }
    }
}
