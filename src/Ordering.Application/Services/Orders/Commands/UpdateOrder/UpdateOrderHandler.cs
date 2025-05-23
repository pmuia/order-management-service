using MediatR;
using Ordering.Application.Data;


namespace Ordering.Application.Services.Orders.Commands.UpdateOrder
{
	public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderResult>
	{
		private readonly IApplicationDbContext _applicationDbContext;
		public UpdateOrderHandler(IApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			var orderId = OrderId.Of(request.Order.Id);

			var order = await _applicationDbContext.Orders.FindAsync(orderId, cancellationToken);

			if (order is null)
			{
				throw new OrderNotFoundException(request.Order.Id.ToString());
			}

			UpdateOrder(order,request.Order);

			_applicationDbContext.Orders.Update(order);

			await _applicationDbContext.SaveChangesAsync(cancellationToken);

			return new UpdateOrderResult(true);
		}

		public void UpdateOrder(Order order, OrderDto orderDto)
		{
			var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

			var billingAddress = Address.Of(orderDto.BillingAddressDto.FirstName, orderDto.BillingAddressDto.LastName, orderDto.BillingAddressDto.EmailAddress, orderDto.BillingAddressDto.AddressLine, orderDto.BillingAddressDto.Country, orderDto.BillingAddressDto.State, orderDto.BillingAddressDto.ZipCode);

			var payment = Payment.Of(orderDto.PaymentDto.CardName, orderDto.PaymentDto.CardName, orderDto.PaymentDto.Expiration, orderDto.PaymentDto.Cvv, orderDto.PaymentDto.PaymentMethod);

			order.Update(OrderName.Of(orderDto.OrderName), shippingAddress, billingAddress, payment, orderDto.Status);
		}
	}
}
