using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using NoticeBoard.Core;
using NoticeBoard.Core.Models;
using NoticeBoard.Core.Models.Domains;
using NoticeBoard.Core.Repository;

namespace NoticeBoard.Persistence.Repository
{
    public class AnnouncementRepository: IAnnouncementRepository
    {
        private IApplicationDbContext _context;

        public AnnouncementRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public byte[] GetImage(int imageId)
        {
            return _context.Images.Single(x => x.Id == imageId).Picture;
        }

        public byte[] GetTitleImage(int id, int titleImageId)
        {
            var imageList = _context.Images.Include(x => x.Announcement).Where(x => x.AnnouncementId == id).ToList();

            if (titleImageId == -1 || titleImageId >= imageList.Count())
                return imageList.FirstOrDefault().Picture;
            else
                return imageList[titleImageId].Picture;
        }

        public int AddAnnouncement(Announcement announcement)
        {
            _context.Announcements.Add(announcement);
            _context.SaveChanges();

            return announcement.Id;
        }

        public Announcement GetAnnouncement(int id, string userId)
        {
            var announcement = _context.Announcements.Single(x => x.Id == id && x.ApplicationUserId == userId);
            announcement.Images = _context.Images.Where(x => x.AnnouncementId == id).ToList();
            return announcement;
        }

        public void AddImages(List<string> newImages, int announcementId)
        {
            for (var i = 0; i < newImages.Count; i++)
            {
                newImages[i] = newImages[i].Remove(0, newImages[i].IndexOf(',') + 1);
                byte[] imgBytes = Convert.FromBase64String(newImages[i]);
                var image = new Image()
                {
                    Picture = imgBytes,
                    AnnouncementId = announcementId,
                };
                _context.Images.Add(image);
            }
        }

        public void EditAnnouncement(Announcement announcement, List<string> newImages, List<int> oldImagesDoNotDelete)
        {
            var announcementToEdit = _context.Announcements.Single(x => x.Id == announcement.Id && x.ApplicationUserId == announcement.ApplicationUserId);

            announcementToEdit.Title = announcement.Title;
            announcementToEdit.CategoryId = announcement.CategoryId;
            announcementToEdit.Description = announcement.Description;
            announcementToEdit.Price = announcement.Price;
            announcementToEdit.TitleImageId = announcement.TitleImageId;

            DeleteImages(announcementToEdit.Id, oldImagesDoNotDelete);
            AddImages(newImages, announcementToEdit.Id);
        }

        public void DeleteImages(int announcementId, List<int> oldImagesDoNotDelete)
        {
            var announcementImages = _context.Images.Where(x => x.AnnouncementId == announcementId).ToList();

            if (announcementImages.Any())
            {
                var imagesToCompare = new List<Image>();

                foreach (var image in oldImagesDoNotDelete)
                    imagesToCompare.Add(_context.Images.Single(x => x.Id == image));

                var imageToDelete = announcementImages.Except(imagesToCompare).ToList();

                foreach (var image in imageToDelete)
                    _context.Images.Remove(image);
            }
        }

        public IEnumerable<Announcement> GetAnnouncements(string userId)
        {
            var announcements = _context.Announcements.Where(x => x.ApplicationUserId == userId).ToList();

            foreach (var announcement in announcements)
                announcement.Images = _context.Images.Where(x => x.AnnouncementId == announcement.Id).ToList();
            return announcements.OrderByDescending(x => x.DateOfCreate);
        }
        public IEnumerable<Announcement> GetAnnouncements(
            string userId, string title = null, int filterId = 0, int categoryId = 0)
        {
            var announcements = _context.Announcements.Where(x => x.ApplicationUserId == userId);

            var announcementsList = FilterAnnouncements(announcements, title, filterId, categoryId);

            foreach (var announcement in announcementsList)
                announcement.Images = _context.Images.Where(x => x.AnnouncementId == announcement.Id).ToList();
            return announcementsList;
        }

        public IEnumerable<Announcement> GetAnnouncements(IEnumerable<Announcement> list, int pageIndex, int pageSize)
        {
            var announcements = new List<Announcement>();
            announcements.AddRange(list.Skip((pageIndex - 1) * pageSize).Take(pageSize));

            return announcements;
        }

        public void DeleteAnnouncement(int id, string userId)
        {
            var announcementToDelete = _context.Announcements.Single(x => x.Id == id && x.ApplicationUserId == userId);
            _context.Announcements.Remove(announcementToDelete);
        }

        public bool CheckIsPersonalDataCreated(string userId)
        {
            if (_context.PersonalDatas.SingleOrDefault(x => x.ApplicationUserId == userId) != null)
                return true;
            else
                return false;
        }

        public IEnumerable<Announcement> GetAllAnnouncements()
        {
            var announcements = _context.Announcements.ToList().OrderByDescending(x => x.DateOfCreate);

            foreach (var announcement in announcements)
                announcement.Images = _context.Images.Where(x => x.AnnouncementId == announcement.Id).ToList();
            return announcements;
        }

        public IEnumerable<Announcement> GetAllAnnouncements(string title = null, int filterId = 0, int categoryId = 0)
        {
            var announcements = _context.Announcements.AsQueryable();

            var announcementsList = FilterAnnouncements(announcements, title, filterId, categoryId);

            foreach (var announcement in announcementsList)
                announcement.Images = _context.Images.Where(x => x.AnnouncementId == announcement.Id).ToList();
            return announcementsList;
        }

        public List<Announcement> FilterAnnouncements(IQueryable<Announcement> announcements, string title = null, int filterId = 0, int categoryId = 0)
        {
            if (categoryId != 0)
                announcements = announcements.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                announcements = announcements.Where(x => x.Title.Contains(title));

            if (filterId != 0)
                switch (filterId)
                {
                    case 1:
                        announcements = announcements.OrderBy(x => x.Price);
                        break;
                    case 2:
                        announcements = announcements.OrderByDescending(x => x.Price);
                        break;
                    case 3:
                        announcements = announcements.OrderByDescending(x => x.DateOfCreate);
                        break;
                    case 4:
                        announcements = announcements.OrderBy(x => x.DateOfCreate);
                        break;
                }
            return announcements.ToList();
        }

        public Announcement GetAnnouncement(int id)
        {
            var announcement = _context.Announcements.Single(x => x.Id == id);
            announcement.Images = _context.Images.Where(x => x.AnnouncementId == id).ToList();
            return announcement;
        }
    }
}
