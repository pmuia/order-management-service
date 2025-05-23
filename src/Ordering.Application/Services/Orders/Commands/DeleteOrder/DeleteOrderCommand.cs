using FluentValidation;
using MediatR;

namespace Ordering.Application.Services.Orders.Commands.DeleteOrder
{
	public record DeleteOrderCommand(Guid OrderId): IRequest<DeleteOrderResult>;

	public record DeleteOrderResult(bool IsSuccess);

	public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
	{
		public DeleteOrderCommandValidator()
		{
			RuleFor(x => x.OrderId).NotEmpty().WithMessage("Order id is required");
		}
	}
}
