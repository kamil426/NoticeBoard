using NoticeBoard.Core.Models.Domains;

namespace NoticeBoard.Core.ViewModels
{
    public class AnnouncementReadOnlyViewModel
    {
        public Announcement Announcement { get; set; }
        public PersonalData PersonalData { get; set; }
    }
}
