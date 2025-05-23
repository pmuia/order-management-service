﻿namespace Ordering.Application.Pagination
{
	public record PaginatedRequest(int PageIndex = 0, int PageSize = 10);
}
