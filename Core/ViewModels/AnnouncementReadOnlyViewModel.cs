using WebAnnouncementsApp.Core.Models.Domains;

namespace WebAnnouncementsApp.Core.ViewModels
{
    public class AnnouncementReadOnlyViewModel
    {
        public Announcement Announcement { get; set; }
        public PersonalData PersonalData { get; set; }
    }
}
