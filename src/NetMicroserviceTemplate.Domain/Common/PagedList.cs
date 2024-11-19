using System.Collections;

namespace NetMicroserviceTemplate.Domain.Common;

public class PagedList<T>(IEnumerable<T> items, int totalItems, int pageNumber, int pageSize)
{
    public IReadOnlyList<T> Items { get; private set; } = items.ToList();
    public int PageNumber { get; private set; } = pageNumber;
    public int PageSize { get; private set; } = pageSize;
    public int TotalItems { get; private set; } = totalItems;
    public int TotalPages => TotalItems > 0 ? (int)Math.Ceiling((double)TotalItems / PageSize) : 0;
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
}
