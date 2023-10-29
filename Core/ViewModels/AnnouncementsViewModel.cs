using NoticeBoard.Core.Models;
using NoticeBoard.Core.Models.Domains;

namespace NoticeBoard.Core.ViewModels
{
    public class AnnouncementsViewModel
    {
        public IEnumerable<Announcement> Announcements { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Filters Filters { get; set; }
        public Pagina Pagina { get; set; }
    }
}
