﻿using MediatR;


namespace Ordering.Application.Services.Orders.Commands.CreateOrder
{
	public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
	{
		private readonly IApplicationDbContext _applicationDbContext;
		public CreateOrderHandler(IApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}
		public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var order = CreateNewOrder(request.Order);

			_applicationDbContext.Orders.Add(order);

			await _applicationDbContext.SaveChangesAsync(cancellationToken);

			return new CreateOrderResult(order.Id.Value);
		}

		private Order CreateNewOrder(OrderDto orderDto)
		{
			var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

			var billingAddress = Address.Of(orderDto.BillingAddressDto.FirstName, orderDto.BillingAddressDto.LastName, orderDto.BillingAddressDto.EmailAddress, orderDto.BillingAddressDto.AddressLine, orderDto.BillingAddressDto.Country, orderDto.BillingAddressDto.State, orderDto.BillingAddressDto.ZipCode);

			var payment = Payment.Of(orderDto.PaymentDto.CardName, orderDto.PaymentDto.CardName, orderDto.PaymentDto.Expiration, orderDto.PaymentDto.Cvv, orderDto.PaymentDto.PaymentMethod);

			var order = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(orderDto.CustomerId), OrderName.Of(orderDto.OrderName), shippingAddress, billingAddress, payment);

			foreach(var orderItemDto in orderDto.OrderItems)
			{
				order.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
			}

			return order;
		}
	}
}
