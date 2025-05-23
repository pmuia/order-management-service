

namespace Ordering.Application.Pagination
{
	public class PaginatedResult<TEntity>(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data) where TEntity : class
	{
		public int pageIndex { get; } = pageIndex;
		public int pageSize { get; } = pageSize;
		public long count { get; } = count;
		public IEnumerable<TEntity> data { get; } = data;
	}
}
