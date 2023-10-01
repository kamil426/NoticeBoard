using WebAnnouncementsApp.Core;
using WebAnnouncementsApp.Core.Models.Domains;
using WebAnnouncementsApp.Core.Service;

namespace WebAnnouncementsApp.Persistence.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnnouncementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int AddAnnouncement(Announcement announcement)
        {
            return _unitOfWork.Announcement.AddAnnouncement(announcement);
        }

        public void AddImages(List<string> newImages, int announcementId)
        {
            _unitOfWork.Announcement.AddImages(newImages, announcementId);
            _unitOfWork.Complete();
        }

        public bool CheckIsPersonalDataCreated(string userId)
        {
            return _unitOfWork.Announcement.CheckIsPersonalDataCreated(userId);
        }

        public void DeleteAnnouncement(int id, string userId)
        {
            _unitOfWork.Announcement.DeleteAnnouncement(id, userId);
            _unitOfWork.Complete();
        }

        public void EditAnnouncement(Announcement announcement, List<string> newImages, List<int> oldImagesDoNotDelete)
        {
            _unitOfWork.Announcement.EditAnnouncement(announcement, newImages, oldImagesDoNotDelete);
            _unitOfWork.Complete();
        }

        public IEnumerable<Announcement> GetAllAnnouncements()
        {
            return _unitOfWork.Announcement.GetAllAnnouncements();
        }

        public IEnumerable<Announcement> GetAllAnnouncements(string title = null, int filterId = 0, int categoryId = 0)
        {
            return _unitOfWork.Announcement.GetAllAnnouncements(title, filterId, categoryId);
        }

        public Announcement GetAnnouncement(int id, string userId)
        {
            return _unitOfWork.Announcement.GetAnnouncement(id, userId);
        }

        public Announcement GetAnnouncement(int id)
        {
            return _unitOfWork.Announcement.GetAnnouncement(id);
        }

        public IEnumerable<Announcement> GetAnnouncements(string userId)
        {
            return _unitOfWork.Announcement.GetAnnouncements(userId);
        }

        public IEnumerable<Announcement> GetAnnouncements(string userId, string title = null, int filterId = 0, int categoryId = 0)
        {
            return _unitOfWork.Announcement.GetAnnouncements(userId, title, filterId, categoryId);
        }

        public IEnumerable<Announcement> GetAnnouncements(IEnumerable<Announcement> list, int pageIndex, int pageSize)
        {
            return _unitOfWork.Announcement.GetAnnouncements(list, pageIndex, pageSize);    
        }

        public byte[] GetImage(int imageId)
        {
            return _unitOfWork.Announcement.GetImage(imageId);
        }

        public byte[] GetTitleImage(int id, int titleImageId)
        {
            return _unitOfWork.Announcement.GetTitleImage(id, titleImageId);
        }
    }
}
