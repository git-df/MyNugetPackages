using DF.Query.Pagination.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DF.Query.Pagination
{
    public static class PaginationHelper
    {
        private const int _page = 1;
        private const int _size = 10;

        public static IQueryable<T> AddPagination<T>(this IQueryable<T> query, Models.Requests.Pagination pagination)
        {
            var page = GetPage(pagination);
            var size = GetSize(pagination);

            return query
                .Skip(size * (page - 1))
                .Take(size);
        }

        public static List<T> AddPagination<T>(this List<T> list, Models.Requests.Pagination pagination)
        {
            var page = GetPage(pagination);
            var size = GetSize(pagination);

            return list
                .Skip(size * (page - 1))
                .Take(size)
                .ToList();
        }

        public static PaginationInfo GetPaginationInfo<T>(this IQueryable<T> query, Models.Requests.Pagination pagination)
        {
            var page = GetPage(pagination);
            var size = GetSize(pagination);
            var totalCount = query.Count();

            return new PaginationInfo
            {
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / size),
                FirstIndex = totalCount == 0 ? 0 : size * (page - 1) + 1,
                LastIndex = Math.Min(size * page, totalCount)
            };
        }

        public static PaginationInfo GetPaginationInfo<T>(this List<T> list, Models.Requests.Pagination pagination)
        {
            var page = GetPage(pagination);
            var size = GetSize(pagination);
            var totalCount = list.Count();

            return new PaginationInfo
            {
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / size),
                FirstIndex = totalCount == 0 ? 0 : size * (page - 1) + 1,
                LastIndex = Math.Min(size * page, totalCount)
            };
        }

        public async static Task<PaginationInfo> GetPaginationInfoAsync<T>(this IQueryable<T> query, 
            Models.Requests.Pagination pagination, CancellationToken cancellation = default)
        {
            var page = GetPage(pagination);
            var size = GetSize(pagination);
            var totalCount = await query.CountAsync(cancellation);

            return new PaginationInfo
            {
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / size),
                FirstIndex = totalCount == 0 ? 0 : size * (page - 1) + 1,
                LastIndex = Math.Min(size * page, totalCount)
            };
        }

        public static PaginatedList<T> GetPaginatedList<T>(this IQueryable<T> query, 
            Models.Requests.Pagination pagination)
        {
            var paginationInfo = query.GetPaginationInfo(pagination);
            query = query.AddPagination(pagination);

            return new PaginatedList<T>(query.ToList(), paginationInfo);
        }

        public async static Task<PaginatedList<T>> GetPaginatedListAsync<T>(this IQueryable<T> query, 
            Models.Requests.Pagination pagination, CancellationToken cancellation = default)
        {
            var paginationInfo = await query.GetPaginationInfoAsync(pagination, cancellation);
            query = query.AddPagination(pagination);

            return new PaginatedList<T>(
                await query.ToListAsync(cancellation), paginationInfo);
        }

        public static PaginatedList<T> GetPaginatedList<T>(this List<T> list,
            Models.Requests.Pagination pagination, CancellationToken cancellation = default)
        {
            var paginationInfo = list.GetPaginationInfo(pagination);
            list = list.AddPagination(pagination);

            return new PaginatedList<T>(
                list, paginationInfo);
        }

        private static int GetPage(Models.Requests.Pagination pagination) 
            => pagination.Page != 0 ? pagination.Page : _page;

        private static int GetSize(Models.Requests.Pagination pagination)
            => pagination.Size != 0 ? pagination.Size : _size;
    }
}
