


namespace Ordering.Application.Services.Orders.Queries.GetOrdersByCustomer
{
	public class GetOrdersByCustomerHandler : IRequestHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
	{
		private readonly IApplicationDbContext _applicationDbContext;
		public GetOrdersByCustomerHandler(IApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}
		public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
		{
			var orders = await _applicationDbContext.Orders.Include(x => x.OrderItems).AsNoTracking()
				.Where(x => x.CustomerId == CustomerId.Of(request.Customerid))
				.OrderBy(x => x.OrderName.Value)
				.ToListAsync(cancellationToken);

			return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
		}
	}
}
