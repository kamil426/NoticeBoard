using WebAnnouncementsApp.Core;
using WebAnnouncementsApp.Core.Models.Domains;
using WebAnnouncementsApp.Core.Service;

namespace WebAnnouncementsApp.Persistence.Services
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

        public PersonalData GetPersonalData(string userId)
        {
            return _unitOfWork.PersonalData.GetPersonalData(userId);
        }

        public PersonalData GetPersonalData(int id)
        {
            return _unitOfWork.PersonalData.GetPersonalData(id);
        }

        public ICollection<PhoneNumber> GetPhoneNumbers(string userId)
        {
            return _unitOfWork.PersonalData.GetPhoneNumbers(userId);
        }
    }
}
