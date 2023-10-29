using NoticeBoard.Core.Models.Domains;

namespace NoticeBoard.Core.Repository
{
    public interface IAnnouncementRepository
    {
        public byte[] GetImage(int imageId);
        public byte[] GetTitleImage(int id, int titleImageId);
        public int AddAnnouncement(Announcement announcement);
        public Announcement GetAnnouncement(int id, string userId);
        public void AddImages(List<string> newImages, int announcementId);
        public void EditAnnouncement(Announcement announcement, List<string> newImages, List<int> oldImagesDoNotDelete);
        public void DeleteImages(int announcementId, List<int> oldImagesDoNotDelete);
        public IEnumerable<Announcement> GetAnnouncements(string userId);
        public IEnumerable<Announcement> GetAnnouncements(string userId, string title = null, int filterId = 0, int categoryId = 0);
        public IEnumerable<Announcement> GetAnnouncements(IEnumerable<Announcement> list, int pageIndex, int pageSize);
        public void DeleteAnnouncement(int id, string userId);
        public bool CheckIsPersonalDataCreated(string userId);
        public IEnumerable<Announcement> GetAllAnnouncements();
        public IEnumerable<Announcement> GetAllAnnouncements(string title = null, int filterId = 0, int categoryId = 0);
        public List<Announcement> FilterAnnouncements(IQueryable<Announcement> announcements, string title = null, int filterId = 0, int categoryId = 0);
        public Announcement GetAnnouncement(int id);
    }
}
