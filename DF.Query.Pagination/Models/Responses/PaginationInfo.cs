using System;
using System.Collections.Generic;
using System.Text;

namespace DF.Query.Pagination.Models.Responses
{
    public class PaginationInfo
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int FirstIndex { get; set; }
        public int LastIndex { get; set; }

        public override string ToString()
        {
            return $"{FirstIndex} - {LastIndex} [{TotalCount}]";
        }

        /// <summary>
        /// Return formated pagination info
        /// </summary>
        /// <param name="format">
        /// [tc] - TotalCount
        /// [tp] - TotalPages
        /// [fi] - FirstIndex
        /// [li] - LastIndex
        /// </param>
        /// <returns></returns>
        public string ToString(string format)
        {
            format = format.Replace("[tc]", TotalCount.ToString());
            format = format.Replace("[tp]", TotalPages.ToString());
            format = format.Replace("[fi]", FirstIndex.ToString());
            format = format.Replace("[li]", LastIndex.ToString());

            return format;
        }

        public string ToString(int page)
        {
            return $"{FirstIndex} - {LastIndex} [{TotalCount}] {page}/{TotalPages}";
        }

        /// <summary>
        /// Return formated pagination info
        /// </summary>
        /// <param name="format">
        /// [tc] - TotalCount
        /// [tp] - TotalPages
        /// [fi] - FirstIndex
        /// [li] - LastIndex
        /// [pg] - Page
        /// </param>
        /// <returns></returns>
        public string ToString(int page, string format)
        {
            format = format.Replace("[tc]", TotalCount.ToString());
            format = format.Replace("[tp]", TotalPages.ToString());
            format = format.Replace("[fi]", FirstIndex.ToString());
            format = format.Replace("[li]", LastIndex.ToString());
            format = format.Replace("[pg]", page.ToString());

            return format;
        }
    }
}
