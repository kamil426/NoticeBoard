using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAnnouncementsApp.Core.Models.Domains;
using WebAnnouncementsApp.Persistence.Repository;
using WebAnnouncementsApp.Persistence;
using WebAnnouncementsApp.Core.ViewModels;
using WebAnnouncementsApp.Persistence.Extensions;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http;
using System.Net;
using WebAnnouncementsApp.Core.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAnnouncementsApp.Core.Service;

namespace WebAnnouncementsApp.Controllers
{
    [Authorize]
    public class AnnouncementController : Controller
    {
        private IAnnouncementService _announcementService;
        private ICategoryService _categoryService;
        private IPersonalDataService _personalDataService;
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private int defaultPageIndex = 1;
        private int defaultPageSize = 30;

        public AnnouncementController(IAnnouncementService announcementService, ICategoryService categoryService, IPersonalDataService personalDataService)
        {
            _announcementService = announcementService;
            _categoryService = categoryService;
            _personalDataService = personalDataService;
        }

        public IActionResult Announcements()
        {
            var userId = User.GetUserId();

            var announcements = _announcementService.GetAnnouncements(userId);
            var pagina = new Pagina();
            pagina.SetProperties(announcements, defaultPageIndex, defaultPageSize);

            var vm = new AnnouncementsViewModel()
            {
                Announcements = new List<Announcement>(_announcementService.GetAnnouncements(announcements, pagina.PageIndex, pagina.PageSize)),
                Categories = new List<Category>(_categoryService.GetCategories()),
                Filters = new Filters(),
                Pagina = pagina
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult FilterAnnouncements(AnnouncementsViewModel viewModel)
        {
            var userId = User.GetUserId();

            IEnumerable<Announcement> announcements;
            Filters filters;
            if (viewModel.Filters == null)
            {
                announcements = _announcementService.GetAnnouncements(userId);
                filters = new Filters();
            }
            else
            {
                announcements = _announcementService.GetAnnouncements(
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
                Announcements = _announcementService.GetAnnouncements(
                    announcements, pagina.PageIndex, pagina.PageSize),
                Categories = _categoryService.GetCategories(),
                Filters = filters,
                Pagina = pagina
            };
            return View("Announcements", vm);
        }

        public IActionResult Announcement(int id = 0)
        {
            var userId = User.GetUserId();

            var isPersonalDataCreated = _announcementService.CheckIsPersonalDataCreated(userId);
            if (isPersonalDataCreated)
            {
                var vm = new AnnouncementViewModel();
                vm.Categories = _categoryService.GetCategories();

                if (id == 0)
                    vm.Announcement = new Announcement()
                    {
                        ApplicationUserId = userId,
                        TitleImageId = -1
                    };
                else
                    vm.Announcement = _announcementService.GetAnnouncement(id, userId);

                return View(vm);
            }
            else
                return View("NotFoundPersonalData");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Announcement
            (Announcement announcement, List<string> newImages, List<int> oldImagesDoNotDelete, int idTitleImage)
        {
            var userId = User.GetUserId();
            announcement.ApplicationUserId = userId;
            announcement.DateOfCreate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                if(announcement.Id != 0)
                    announcement.Images = _announcementService.GetAnnouncement(announcement.Id, userId).Images;
                AnnouncementViewModel vm;
                if(newImages != null)
                    vm = new AnnouncementViewModel()
                    {
                        Categories = _categoryService.GetCategories(),
                        Announcement = announcement,
                        NewImages = new List<string>(newImages),
                    };
                else
                    vm = new AnnouncementViewModel()
                    {
                        Categories = _categoryService.GetCategories(),
                        Announcement = announcement,
                    };

                return View(vm);
            }
            announcement.TitleImageId = idTitleImage;

            if (announcement.Id == 0)
            {
                var announcementId = _announcementService.AddAnnouncement(announcement);
                if (newImages.Any())
                    _announcementService.AddImages(newImages, announcementId);
            }
            else
                _announcementService.EditAnnouncement(announcement, newImages, oldImagesDoNotDelete);

            return RedirectToAction("Announcements", "Announcement");
        }

        [AllowAnonymous]
        public IActionResult AnnouncementReadOnly(int id)
        {
            var vm = new AnnouncementReadOnlyViewModel()
            {
                Announcement = _announcementService.GetAnnouncement(id),
                PersonalData = _personalDataService.GetPersonalData(id)
            };

            return View(vm);
        }

        [AllowAnonymous]
        public IActionResult AllAnnouncements()
        {
            var announcements = _announcementService.GetAllAnnouncements();
            var pagina = new Pagina();
            pagina.SetProperties(announcements, defaultPageIndex, defaultPageSize);

            var vm = new AnnouncementsViewModel()
            {
                Announcements = _announcementService.GetAnnouncements(announcements, defaultPageIndex, defaultPageSize),
                Categories = _categoryService.GetCategories(),
                Filters = new Filters(),
                Pagina = pagina
            };

            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult FilterAllAnnouncements(AnnouncementsViewModel viewModel)
        {
            IEnumerable<Announcement> announcements;
            Filters filters;
            if (viewModel.Filters == null)
            {
                announcements = _announcementService.GetAllAnnouncements();
                filters = new Filters();
            }
            else
            {
                announcements = _announcementService.GetAllAnnouncements(
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
                Announcements = _announcementService.GetAnnouncements(
                    announcements, pagina.PageIndex, pagina.PageSize),
                Categories = _categoryService.GetCategories(),
                Filters = filters,
                Pagina = pagina
            };

            return View("AllAnnouncements", vm);
        }

        [AllowAnonymous]
        public IActionResult GetImage(int id)
        {
            var img = _announcementService.GetImage(id);

            return File(img, "image/jpg");
        }

        [AllowAnonymous]
        public IActionResult GetTitleImage(int id, int titleImageId)
        {
            var img = _announcementService.GetTitleImage(id, titleImageId);

            return File(img, "image/jpg");
        }

        [HttpPost]
        public IActionResult DeleteAnnouncement(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _announcementService.DeleteAnnouncement(id, userId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return Json(new { Success = false, Message = ex.Message });
            }
            return Json(new { Success = true });
        }
    }
}
