using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSet.Core
{
    public class PagedList <T>
    {
        public PagedList(IList<T> items, int totalItems, int currentPage, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        }

        public int TotalItems { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public IList<T> Items { get; }
    }
}
