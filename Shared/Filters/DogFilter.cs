namespace Shared.Filters {
    public record DogFilter {
        public DogAttributeSort? AttributeSort { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public SortOrder SortOrder { get; init; }

        public DogFilter(
            DogAttributeSort? attributeSort = null,
            int pageNumber = 1,
            int pageSize = 5,
            SortOrder sortOrder = SortOrder.Ascending) {
            AttributeSort = attributeSort;
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? 5 : pageSize;
            SortOrder = sortOrder;
        }
    }
}
