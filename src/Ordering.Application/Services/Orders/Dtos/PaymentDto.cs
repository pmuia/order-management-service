

namespace Ordering.Application.Services.Orders.Dtos
{
	public record PaymentDto(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);
}
