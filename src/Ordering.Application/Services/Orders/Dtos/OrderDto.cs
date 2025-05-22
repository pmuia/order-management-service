using Ordering.Domain.Enums;

namespace Ordering.Application.Services.Orders.Dtos
{
	public record OrderDto(
		Guid Id,
		Guid CustomerId,
		string OrderName,
		ShippingAddressDto ShippingAddress,
		BillingAddressDto BillingAddressDto,
		PaymentDto PaymentDto,
		OrderStatus Status,
		List<OrderItemDto> OrderItems);
}
