using System;
using System.Collections.Generic;
using System.Text;

namespace DF.Query.Pagination.Models.Responses
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public PaginationInfo Pagination { get; set; }

        public PaginatedList(List<T> items, PaginationInfo paginationInfo)
        {
            Items = items;
            Pagination = paginationInfo;
        }
    }
}
