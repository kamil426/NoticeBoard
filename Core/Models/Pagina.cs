using System.Collections.Generic;
using System.Drawing.Printing;
using WebAnnouncementsApp.Core.Models.Domains;

namespace WebAnnouncementsApp.Core.Models
{
    public class Pagina
    {
        public void SetProperties(IEnumerable<Announcement> list, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = list.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
