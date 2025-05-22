

using Microsoft.Extensions.Configuration;
using Ordering.Application.Common.Interface;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System.Collections.Generic;
using System.Data;

namespace Ordering.Infrastructure.Data
{
	public class Seed : ISeed
	{
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;

		public Seed(ApplicationDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		public async Task SeedDefaults()
		{
			if (!await _context.Customers.AnyAsync())
			{
				var customer = Customer.Create(
				   CustomerId.Of(Guid.NewGuid()),
				   "Paul Muia",
				   "paul.muia@gmail.com"
			   );

				await _context.Customers.AddAsync(customer);
			}

			if (!await _context.Products.AnyAsync())
			{
				var products = new List<Product>
				{
					 Product.Create(ProductId.Of(Guid.NewGuid()), "d.Light D200 Solar System", 11999),
					  Product.Create(ProductId.Of(Guid.NewGuid()), "d.Light D100 Solar System", 7999),
					   Product.Create(ProductId.Of(Guid.NewGuid()), "d.Light D300 Solar System", 15999),
				};

				await _context.Products.AddRangeAsync(products);
			}

			if (!await _context.Orders.AnyAsync())
			{
				var customers = await _context.Customers.ToListAsync();
				var orders = new List<Order>();

				foreach (var customer in customers)
				{
					for (int i = 0; i < 3; i++)
					{
						var shippingAddress = Address.Of("Paul Muia", "Muthama", "muthama54@gmail.com", "Fedha Embakasi", "Kenya", "Nairobi", "69120");
						var billingAddress = Address.Of("Paul Muia", "Muthama", "muthama54@gmail.com", "Fedha Embakasi", "Kenya", "Nairobi", "69120");
						var payment = Payment.Of("Paul Muia Muthama","42424421421421","2703","324",1);

						var order = Order.Create(OrderId.Of(Guid.NewGuid()), customer.Id, OrderName.Of("ORD-1"), shippingAddress, billingAddress, payment);

						order.Add(ProductId.Of(new Guid("3C05A6BF-10E0-4D07-A1AD-0850C66DECDA")), 8, 1199);
						

						orders.Add(order);
					}
				}

				await _context.Orders.AddRangeAsync(orders);
			}

			await _context.SaveChangesAsync();
		}
	}
}
