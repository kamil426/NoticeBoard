using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoticeBoard.Core.Models.Domains;
using NoticeBoard.Core.ViewModels;
using NoticeBoard.Persistence.Extensions;
using NoticeBoard.Core.Models;
using NoticeBoard.Core.Service;

namespace NoticeBoard.Controllers
{
    [Authorize]
    public class AnnouncementController : Controller
    {
        private IAnnouncementService _announcementService;
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        public IActionResult Announcements()
        {
            var userId = User.GetUserId();

            var vm = _announcementService.GetAnnouncements(userId);

            return View(vm);
        }

        [HttpPost]
        public IActionResult FilterAnnouncements(AnnouncementsViewModel viewModel)
        {
            var userId = User.GetUserId();

            var vm = _announcementService.GetFilterAnnouncements(viewModel, userId);
            
            return View("Announcements", vm);
        }

        public IActionResult Announcement(int id = 0)
        {
            var userId = User.GetUserId();

            var isPersonalDataCreated = _announcementService.CheckIsPersonalDataCreated(userId);
            if (isPersonalDataCreated)
            {
                var vm = _announcementService.GetNewAnnouncement(userId, id);

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
                var vm = _announcementService.GetInvalidAnnouncement(announcement, newImages);

                return View(vm);
            }

            _announcementService.AddEditAnnouncement(announcement, newImages, oldImagesDoNotDelete, idTitleImage);

            return RedirectToAction("Announcements", "Announcement");
        }

        [AllowAnonymous]
        public IActionResult AnnouncementReadOnly(int id)
        {
            var vm = _announcementService.GetAnnouncementReadOnly(id);

            return View(vm);
        }

        [AllowAnonymous]
        public IActionResult AllAnnouncements()
        {
            var vm = _announcementService.GetAllAnnouncements();

            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult FilterAllAnnouncements(AnnouncementsViewModel viewModel)
        {
            var vm = _announcementService.FilterAllAnnouncements(viewModel);

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
            var userId = User.GetUserId();
            try
            {
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
