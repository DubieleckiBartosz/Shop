

namespace Application.Wrapper
{
    public class PagedResponse<T>:ServiceResponse<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; } 
        public PagedResponse(T data,int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            CurrentPage = pageNumber;
            Data = data;
            Success = true;
        }
    }
}
