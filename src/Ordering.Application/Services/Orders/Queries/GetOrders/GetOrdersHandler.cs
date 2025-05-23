


namespace Ordering.Application.Services.Orders.Queries.GetOrders
{
	public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, GetOrdersResult>
	{
		private readonly IApplicationDbContext _applicationDbContext;
		public GetOrdersHandler(IApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}
		public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
		{
			var totalCount = await _applicationDbContext.Orders.LongCountAsync(cancellationToken);

			var orders = await _applicationDbContext.Orders.Include(x=>x.OrderItems)
				.OrderBy(x=>x.OrderName.Value)
				.Skip(request.PaginatedRequest.PageSize * request.PaginatedRequest.PageIndex)
				.Take(request.PaginatedRequest.PageSize)
				.ToListAsync(cancellationToken);

			return new GetOrdersResult(new Pagination.PaginatedResult<OrderDto>(request.PaginatedRequest.PageIndex, request.PaginatedRequest.PageSize, totalCount, orders.ToOrderDtoList()));
		}
	}
}
