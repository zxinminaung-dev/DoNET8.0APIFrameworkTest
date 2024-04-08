namespace ChatApp.Web.Infrastructure
{
    public class PageResult<TEntity>
    {
        public List<TEntity>? data { get; set; }
        public int total { get; set; }
        public string? filterBy { get; set; }
    }
}
