using Ordering.Application.Pagination;

namespace Ordering.Application.Services.Orders.Queries.GetOrders
{
	public record GetOrdersQuery(PaginatedRequest PaginatedRequest):IRequest<GetOrdersResult>;

	public record GetOrdersResult(PaginatedResult<OrderDto> orders);
}
