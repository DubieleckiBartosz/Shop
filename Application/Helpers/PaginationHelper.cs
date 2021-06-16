using Application.Filters;
using Application.Wrapper;
using System;
using System.Collections.Generic;

namespace Application.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<IEnumerable<T>> CreatePagedResponse<T>(IEnumerable<T> pagedData,AbstractQueryStringParameters parameters,int totalRecords)
        {
            var response = new PagedResponse<IEnumerable<T>>(pagedData, parameters.PageNumber, parameters.PageSize);
            var totalPages = (int)Math.Ceiling(totalRecords / (double)parameters.PageSize);
            var currentPage = parameters.PageNumber;
            response.TotalPages = totalPages;
            response.TotalCount = totalRecords;
            response.HasPrevious = currentPage>1 ? true : false;
            response.HasNext = currentPage < totalPages ? true : false;
            return response;
        }
    }
}
