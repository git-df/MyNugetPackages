using DF.Query.Pagination.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DF.Query.Pagination.Models.Requests
{
    public class Pagination
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public IQueryable<T> AddPagination<T>(IQueryable<T> query) =>
            query.AddPagination(this);

        public List<T> AddPagination<T>(List<T> query) =>
            query.AddPagination(this);

        public PaginationInfo GetPaginationInfo<T>(IQueryable<T> query) =>
            query.GetPaginationInfo(this);

        public PaginationInfo GetPaginationInfo<T>(List<T> query) =>
            query.GetPaginationInfo(this);

        public async Task<PaginationInfo> GetPaginationInfoAsync<T>(IQueryable<T> query,
            CancellationToken cancellationToken = default) =>
            await query.GetPaginationInfoAsync(this, cancellationToken);

        public PaginatedList<T> GetPaginatedList<T>(IQueryable<T> query) =>
            query.GetPaginatedList(this);

        public async Task<PaginatedList<T>> GetPaginatedListAsync<T>(IQueryable<T> query,
            CancellationToken cancellationToken = default) =>
            await query.GetPaginatedListAsync(this, cancellationToken);

        public PaginatedList<T> GetPaginatedList<T>(List<T> query) =>
            query.GetPaginatedList(this);
    }
}
