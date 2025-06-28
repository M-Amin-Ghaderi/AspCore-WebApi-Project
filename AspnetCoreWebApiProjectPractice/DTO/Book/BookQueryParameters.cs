namespace AspnetCoreWebApiProjectPractice.DTO.Book
{
    public class BookQueryParameters
    {
        private const int maxPageSize = 50;
        public int Page { get; set; } = 1;

        private int pageSize = 10;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string? Search { get; set; }
    }
}
