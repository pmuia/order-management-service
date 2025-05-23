

using Ordering.Application.Extensions;

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
				.OrderBy(x => x.OrderName.Value)
				.ToListAsync(cancellationToken);

			return new GetOrderByNameResult(orders.ToOrderDtoList());
		}		
	}
}
