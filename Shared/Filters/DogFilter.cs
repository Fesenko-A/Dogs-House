namespace Shared.Filters {
    public record DogFilter(DogAttributeSort AttributeSort, SortOrder SortOrder, int PageNumber = 1, int PageSize = 5);
}
