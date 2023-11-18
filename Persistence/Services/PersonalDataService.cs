using NoticeBoard.Core;
using NoticeBoard.Core.Models.Domains;
using NoticeBoard.Core.Service;
using NoticeBoard.Core.ViewModels;

namespace NoticeBoard.Persistence.Services
{
    public class PersonalDataService: IPersonalDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PersonalDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddEditPersonalData(PersonalData personalData, List<string> newPhoneNumbers, List<string> phoneNumbersDoNotDelete)
        {
            _unitOfWork.PersonalData.AddEditPersonalData(personalData, newPhoneNumbers, phoneNumbersDoNotDelete);
            _unitOfWork.Complete();
        }

        public PersonalDataViewModel GetPersonalData(string userId)
        {
            var personalData = _unitOfWork.PersonalData.GetPersonalData(userId);
            personalData.PhoneNumbers = _unitOfWork.PersonalData.GetPhoneNumbers(userId);

            var vm = new PersonalDataViewModel() { PersonalData = personalData };

            return vm;
        }
        public PersonalDataViewModel GetInvalidPersonalData(PersonalData personalData, List<string> newPhoneNumbers)
        {
            personalData.PhoneNumbers = _unitOfWork.PersonalData.GetPhoneNumbers(personalData.ApplicationUserId);
            PersonalDataViewModel vm;
            if (newPhoneNumbers != null)
                vm = new PersonalDataViewModel()
                {
                    PersonalData = personalData,
                    NewPhoneNumbers = new List<string>(newPhoneNumbers)
                };
            else
                vm = new PersonalDataViewModel() { PersonalData = personalData };

            return vm;
        }
    }
}
