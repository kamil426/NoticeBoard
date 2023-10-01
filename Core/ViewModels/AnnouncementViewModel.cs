using WebAnnouncementsApp.Core.Models.Domains;

namespace WebAnnouncementsApp.Core.ViewModels
{
    public class AnnouncementViewModel
    {
        public Announcement Announcement { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public List<string>? NewImages { get; set; }
    }
}
