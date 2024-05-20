namespace Hospital.Application.Common
{
    public class PaginatedResult<T> where T : class
    {
        public int TotalItems { get; init; }
        public IList<T> Items { get; init; }
    }
}
