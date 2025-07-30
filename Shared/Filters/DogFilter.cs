namespace Shared.Filters {
    public class DogFilter {
        public DogAttributeSort? AttributeSort { get; set; }
        public SortOrder? SortOrder { get; set; }

        private int _pageNumber = 1;
        public int PageNumber {
            get => _pageNumber <= 0 ? 1 : _pageNumber;
            set => _pageNumber = value;
        }

        private int _pageSize = 5;
        public int PageSize {
            get => _pageSize <= 0 ? 5 : _pageSize;
            set => _pageSize = value;
        }
    }
}
