

namespace Ordering.Application.Services.Orders.Queries.GetOrderByName
{
	public record GetOrdersByNameQuery(string Name) : IRequest<GetOrderByNameResult>;

	public record GetOrderByNameResult(IEnumerable<OrderDto> Orders);
}
