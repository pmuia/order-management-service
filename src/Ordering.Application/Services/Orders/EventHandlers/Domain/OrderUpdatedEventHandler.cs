

namespace Ordering.Application.Services.Orders.EventHandlers.Domain
{
	public class OrderUpdatedEventHandler : INotificationHandler<OrderUpdateEvent>
	{
		private readonly ILogger<OrderUpdatedEventHandler> _logger;
		public OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
		{
			_logger = logger;
		}
		public Task Handle(OrderUpdateEvent notification, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Domain Event handled: {Domain Event}", notification.GetType().Name);

			return Task.CompletedTask;
		}
	}
}
