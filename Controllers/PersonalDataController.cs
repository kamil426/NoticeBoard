using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using NoticeBoard.Core.Models.Domains;
using NoticeBoard.Core.Repository;
using NoticeBoard.Core.Service;
using NoticeBoard.Core.ViewModels;
using NoticeBoard.Migrations;
using NoticeBoard.Persistence;
using NoticeBoard.Persistence.Extensions;
using NoticeBoard.Persistence.Repository;

namespace NoticeBoard.Controllers
{
    [Authorize]
    public class PersonalDataController : Controller
    {
        private IPersonalDataService _personalDataService;
        public PersonalDataController(IPersonalDataService personalDataService)
        {
            _personalDataService = personalDataService;
        }
        public IActionResult PersonalData()
        {
            var userId = User.GetUserId();

            var personalData = _personalDataService.GetPersonalData(userId);
            personalData.PhoneNumbers = _personalDataService.GetPhoneNumbers(userId);

            var vm = new PersonalDataViewModel() { PersonalData = personalData };

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddEditPersonalData(PersonalData personalData, List<string> newPhoneNumbers, List<string> phoneNumbersDoNotDelete)
        {
            var userId = User.GetUserId();
            personalData.ApplicationUserId = userId;

            if (!ModelState.IsValid)
            {
                personalData.PhoneNumbers = _personalDataService.GetPhoneNumbers(userId);
                PersonalDataViewModel vm;
                if(newPhoneNumbers != null)
                    vm = new PersonalDataViewModel()
                    {
                        PersonalData = personalData,
                        NewPhoneNumbers = new List<string>(newPhoneNumbers)
                    };
                else 
                    vm = new PersonalDataViewModel() { PersonalData = personalData };

                return View("PersonalData", vm);
            }

            _personalDataService.AddEditPersonalData(personalData, newPhoneNumbers, phoneNumbersDoNotDelete);

            return RedirectToAction("Index", "Home");
        }
    }
}
