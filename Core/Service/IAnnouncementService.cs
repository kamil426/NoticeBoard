using Microsoft.AspNetCore.Mvc;
using NoticeBoard.Core.Models.Domains;
using NoticeBoard.Core.ViewModels;

namespace NoticeBoard.Core.Service
{
    public interface IAnnouncementService
    {
        public bool CheckIsPersonalDataCreated(string userId);
        public void DeleteAnnouncement(int id, string userId);
        public byte[] GetImage(int imageId);
        public byte[] GetTitleImage(int id, int titleImageId);
        public AnnouncementsViewModel GetAnnouncements(string userId);
        public AnnouncementsViewModel GetFilterAnnouncements(AnnouncementsViewModel viewModel, string userId);
        public AnnouncementViewModel GetNewAnnouncement(string userId, int id = 0);
        public AnnouncementViewModel GetInvalidAnnouncement(Announcement announcement, List<string> newImages);
        public void AddEditAnnouncement(Announcement announcement, List<string> newImages, List<int> oldImagesDoNotDelete, int idTitleImage);
        public AnnouncementReadOnlyViewModel GetAnnouncementReadOnly(int id);
        public AnnouncementsViewModel GetAllAnnouncements();
        public AnnouncementsViewModel FilterAllAnnouncements(AnnouncementsViewModel viewModel);
    }
}
