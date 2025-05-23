
namespace Ordering.Application.Services.Orders.Queries.GetOrdersByCustomer
{

	public record GetOrdersByCustomerQuery(Guid Customerid) : IRequest<GetOrdersByCustomerResult>;

	public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);
}
