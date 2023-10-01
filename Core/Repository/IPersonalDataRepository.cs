using Microsoft.EntityFrameworkCore;
using WebAnnouncementsApp.Core.Models.Domains;

namespace WebAnnouncementsApp.Core.Repository
{
    public interface IPersonalDataRepository
    {
        public PersonalData GetPersonalData(string userId);
        public ICollection<PhoneNumber> GetPhoneNumbers(string userId);
        public void AddEditPersonalData(PersonalData personalData, List<string> newPhoneNumbers, List<string> phoneNumbersDoNotDelete);
        public void DeletePhoneNumbers(List<string> phoneNumbersDoNotDelete, PersonalData personalData);
        public void EditPersonalData(PersonalData personalData, List<PhoneNumber> phoneNumbersToAdd);
        public void AddPersonalData(PersonalData personalData, List<string> newPhoneNumbers);
        public PersonalData GetPersonalData(int id);
    }
}
