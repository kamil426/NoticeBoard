using NoticeBoard.Core.Models.Domains;

namespace NoticeBoard.Core.ViewModels
{
    public class AnnouncementViewModel
    {
        public Announcement Announcement { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public List<string>? NewImages { get; set; }
    }
}
