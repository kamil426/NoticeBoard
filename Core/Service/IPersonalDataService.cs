using NoticeBoard.Core.Models.Domains;
using NoticeBoard.Core.ViewModels;

namespace NoticeBoard.Core.Service
{
    public interface IPersonalDataService
    {
        public void AddEditPersonalData(PersonalData personalData, List<string> newPhoneNumbers, List<string> phoneNumbersDoNotDelete);
        public PersonalDataViewModel GetPersonalData(string userId);
        public PersonalDataViewModel GetInvalidPersonalData(PersonalData personalData, List<string> newPhoneNumbers);
    }
}
