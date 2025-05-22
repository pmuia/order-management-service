using Ordering.Application.Services.Orders.Dtos;
using FluentValidation;
using MediatR;

namespace Ordering.Application.Services.Orders.Commands.CreateOrder
{
	public record CreateOrderCommand(OrderDto Order) : IRequest<CreateOrderResult>;

	public record CreateOrderResult(Guid Id);

	public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator()
		{
			RuleFor(x=>x.Order.OrderName).NotEmpty().WithMessage("Name is required");
			RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
			RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
		}
	}


}
