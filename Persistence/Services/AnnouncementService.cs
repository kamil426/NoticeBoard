using Microsoft.AspNetCore.Mvc;
using NoticeBoard.Core;
using NoticeBoard.Core.Models;
using NoticeBoard.Core.Models.Domains;
using NoticeBoard.Core.Service;
using NoticeBoard.Core.ViewModels;

namespace NoticeBoard.Persistence.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private int defaultPageIndex = 1;
        private int defaultPageSize = 30;
        public AnnouncementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AnnouncementsViewModel GetAnnouncements(string userId)
        {
            var announcements = _unitOfWork.Announcement.GetAnnouncements(userId);
            var pagina = new Pagina();
            pagina.SetProperties(announcements, defaultPageIndex, defaultPageSize);

            var vm = new AnnouncementsViewModel()
            {
                Announcements = new List<Announcement>(_unitOfWork.Announcement.GetAnnouncements(announcements, pagina.PageIndex, pagina.PageSize)),
                Categories = new List<Category>(_unitOfWork.Category.GetCategories()),
                Filters = new Filters(),
                Pagina = pagina
            };
            return vm;
        }

        public AnnouncementsViewModel GetFilterAnnouncements(AnnouncementsViewModel viewModel, string userId)
        {
            IEnumerable<Announcement> announcements;
            Filters filters;
            if (viewModel.Filters == null)
            {
                announcements = _unitOfWork.Announcement.GetAnnouncements(userId);
                filters = new Filters();
            }
            else
            {
                announcements = _unitOfWork.Announcement.GetAnnouncements(
                    userId, viewModel.Filters.Title, viewModel.Filters.FilterId, viewModel.Filters.CategoryId);
                filters = new Filters()
                {
                    CategoryId = viewModel.Filters.CategoryId,
                    FilterId = viewModel.Filters.FilterId,
                    Title = viewModel.Filters.Title
                };
            }

            Pagina pagina = new Pagina();
            if (viewModel.Pagina == null)
                pagina.SetProperties(announcements, defaultPageIndex, defaultPageSize);
            else
                pagina.SetProperties(announcements, viewModel.Pagina.PageIndex, viewModel.Pagina.PageSize);

            var vm = new AnnouncementsViewModel()
            {
                Announcements = _unitOfWork.Announcement.GetAnnouncements(
                    announcements, pagina.PageIndex, pagina.PageSize),
                Categories = _unitOfWork.Category.GetCategories(),
                Filters = filters,
                Pagina = pagina
            };

            return vm;
        }

        public bool CheckIsPersonalDataCreated(string userId)
        {
            return _unitOfWork.Announcement.CheckIsPersonalDataCreated(userId);
        }

        public byte[] GetImage(int imageId)
        {
            return _unitOfWork.Announcement.GetImage(imageId);
        }

        public byte[] GetTitleImage(int id, int titleImageId)
        {
            return _unitOfWork.Announcement.GetTitleImage(id, titleImageId);
        }
        public void DeleteAnnouncement(int id, string userId)
        {
            _unitOfWork.Announcement.DeleteAnnouncement(id, userId);
            _unitOfWork.Complete();
        }

        public AnnouncementViewModel GetNewAnnouncement(string userId, int id = 0)
        {
            var vm = new AnnouncementViewModel();
            vm.Categories = _unitOfWork.Category.GetCategories();

            if (id == 0)
                vm.Announcement = new Announcement()
                {
                    ApplicationUserId = userId,
                    TitleImageId = -1
                };
            else
                vm.Announcement = _unitOfWork.Announcement.GetAnnouncement(id, userId);

            return vm;
        }

        public AnnouncementViewModel GetInvalidAnnouncement(Announcement announcement, List<string> newImages)
        {
            if (announcement.Id != 0)
                announcement.Images = _unitOfWork.Announcement.GetAnnouncement(announcement.Id, announcement.ApplicationUserId).Images;
            AnnouncementViewModel vm;
            if (newImages != null)
                vm = new AnnouncementViewModel()
                {
                    Categories = _unitOfWork.Category.GetCategories(),
                    Announcement = announcement,
                    NewImages = new List<string>(newImages),
                };
            else
                vm = new AnnouncementViewModel()
                {
                    Categories = _unitOfWork.Category.GetCategories(),
                    Announcement = announcement,
                };
            return vm;
        }
        public void AddEditAnnouncement(Announcement announcement, List<string> newImages, List<int> oldImagesDoNotDelete, int idTitleImage)
        {
            announcement.TitleImageId = idTitleImage;

            if (announcement.Id == 0)
            {
                var announcementId = _unitOfWork.Announcement.AddAnnouncement(announcement);
                if (newImages.Any())
                {
                    _unitOfWork.Announcement.AddImages(newImages, announcementId);
                    _unitOfWork.Complete();
                }
            }
            else
            {
                _unitOfWork.Announcement.EditAnnouncement(announcement, newImages, oldImagesDoNotDelete);
                _unitOfWork.Complete();
            }
        }
        public AnnouncementReadOnlyViewModel GetAnnouncementReadOnly(int id)
        {
            var vm = new AnnouncementReadOnlyViewModel()
            {
                Announcement = _unitOfWork.Announcement.GetAnnouncement(id),
                PersonalData = _unitOfWork.PersonalData.GetPersonalData(id)
            };

            return vm;
        }
        public AnnouncementsViewModel GetAllAnnouncements()
        {
            var announcements = _unitOfWork.Announcement.GetAllAnnouncements();
            var pagina = new Pagina();
            pagina.SetProperties(announcements, defaultPageIndex, defaultPageSize);

            var vm = new AnnouncementsViewModel()
            {
                Announcements = _unitOfWork.Announcement.GetAnnouncements(announcements, defaultPageIndex, defaultPageSize),
                Categories = _unitOfWork.Category.GetCategories(),
                Filters = new Filters(),
                Pagina = pagina
            };

            return vm;
        }
        public AnnouncementsViewModel FilterAllAnnouncements(AnnouncementsViewModel viewModel)
        {
            IEnumerable<Announcement> announcements;
            Filters filters;
            if (viewModel.Filters == null)
            {
                announcements = _unitOfWork.Announcement.GetAllAnnouncements();
                filters = new Filters();
            }
            else
            {
                announcements = _unitOfWork.Announcement.GetAllAnnouncements(
                    viewModel.Filters.Title, viewModel.Filters.FilterId, viewModel.Filters.CategoryId);
                filters = new Filters()
                {
                    CategoryId = viewModel.Filters.CategoryId,
                    FilterId = viewModel.Filters.FilterId,
                    Title = viewModel.Filters.Title
                };
            }

            Pagina pagina = new Pagina();
            if (viewModel.Pagina == null)
                pagina.SetProperties(announcements, defaultPageIndex, defaultPageSize);
            else
                pagina.SetProperties(announcements, viewModel.Pagina.PageIndex, viewModel.Pagina.PageSize);

            var vm = new AnnouncementsViewModel()
            {
                Announcements = _unitOfWork.Announcement.GetAnnouncements(
                    announcements, pagina.PageIndex, pagina.PageSize),
                Categories = _unitOfWork.Category.GetCategories(),
                Filters = filters,
                Pagina = pagina
            };

            return vm;
        }
    }
}
