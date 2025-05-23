using MediatR;

namespace Ordering.Application.Services.Orders.Commands.DeleteOrder
{
	public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, DeleteOrderResult>
	{
		private readonly IApplicationDbContext _applicationDbContext;
		public DeleteOrderHandler(IApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}
		public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
		{
			var orderId = OrderId.Of(request.OrderId);

			var order = await _applicationDbContext.Orders.FindAsync(orderId, cancellationToken);

			if (order is null)
			{
				throw new OrderNotFoundException(request.OrderId.ToString());
			}

			_applicationDbContext.Orders.Remove(order);

			await _applicationDbContext.SaveChangesAsync(cancellationToken);

			return new DeleteOrderResult(true);
		}
	}
}
