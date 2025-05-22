using MediatR;

namespace Ordering.Application.Services.Orders.Commands.CreateOrder
{
	public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
	{
		public Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
