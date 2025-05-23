

namespace Ordering.Application.Services.Orders.EventHandlers.Domain
{
	public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
	{
		private readonly ILogger<OrderCreatedEventHandler> _logger;
		public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
		{
			_logger = logger;
		}
		public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Domain Event handled: {Domain Event}", notification.GetType().Name);

			return Task.CompletedTask;
		}
	}
}
