﻿namespace Ordering.Application.Services.Orders.Dtos
{
	public record BillingAddressDto(string FirstName, string LastName, string EmailAddress, string AddressLine, string Country, string State, string ZipCode);
}
