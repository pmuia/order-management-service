﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.EntityConfiguration
{
	public class ProductConfiguration:IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).HasConversion(productId => productId.Value, dbId => ProductId.Of(dbId));

			builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
		}
	}
}
