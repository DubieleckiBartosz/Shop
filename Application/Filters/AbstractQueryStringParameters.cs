

namespace Application.Filters
{
    public abstract class AbstractQueryStringParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize=10;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value>maxPageSize)?maxPageSize:value; }
        }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
    public class ProductParameters : AbstractQueryStringParameters
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public bool ValidPriceParameters => MaxPrice > MinPrice;
        public string Name { get; set; }
    }
}
