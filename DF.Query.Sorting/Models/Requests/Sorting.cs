using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF.Query.Sorting.Models.Requests
{
    public class Sorting
    {
        public string OrderBy { get; set; } = string.Empty;
        public bool Descending { get; set; }

        public IOrderedQueryable<T> AddSorting<T>(IQueryable<T> query) =>
            query.AddSorting(this);

        public List<T> AddSorting<T>(List<T> query) =>
            query.AddSorting(this);
    }
}
