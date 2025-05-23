

namespace Ordering.Application.Services.Orders.Queries.GetOrderByName
{
	public class GetOrdersByNameHnadler : IRequestHandler<GetOrdersByNameQuery, GetOrderByNameResult>
	{
		private readonly IApplicationDbContext _applicationDbContext;
		public GetOrdersByNameHnadler(IApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}
		public async Task<GetOrderByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
		{
			var orders = await _applicationDbContext.Orders.Include(x => x.OrderItems).AsNoTracking()
				.Where(x => x.OrderName.Value.Contains(request.Name))
				.OrderBy(x => x.OrderName)
				.ToListAsync(cancellationToken);

			var orderDtos = ProjectToorderDto(orders);

			return new GetOrderByNameResult(orderDtos);
		}

		private List<OrderDto> ProjectToorderDto(List<Order> orders)
		{
			List<OrderDto> result = new();
			foreach (var order in orders)
			{
				var orderDto = new OrderDto(
					order.Id.Value,
					order.CustomerId.Value,
					order.OrderName.Value,
					new ShippingAddressDto(
					order.ShippingAddress.FirstName,
					order.ShippingAddress.LastName,
					order.ShippingAddress.EmailAddress,
					order.ShippingAddress.AddressLine,
					order.ShippingAddress.Country,
					order.ShippingAddress.State,
					order.ShippingAddress.ZipCode),
					new BillingAddressDto(
						order.BillingAddress.FirstName,
					order.BillingAddress.LastName,
					order.BillingAddress.EmailAddress,
					order.BillingAddress.AddressLine,
					order.BillingAddress.Country,
					order.BillingAddress.State,
					order.BillingAddress.ZipCode),
					new PaymentDto(order.Payment.CardName,
					order.Payment.CardName,
					order.Payment.Expiration,
					order.Payment.CVV,
					order.Payment.PaymentMethod),
					order.Status,
					order.OrderItems.Select(x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.Quantity, x.Price)).ToList()
					);

				result.Add(orderDto);
			}

			return result;
		}
	}
}
