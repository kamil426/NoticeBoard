using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAnnouncementsApp.Core;
using WebAnnouncementsApp.Core.Models.Domains;
using WebAnnouncementsApp.Core.Repository;

namespace WebAnnouncementsApp.Persistence.Repository
{
    public class PersonalDataRepository: IPersonalDataRepository
    {
        private IApplicationDbContext _context;
        public PersonalDataRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public PersonalData GetPersonalData(string userId)
        {
            if (_context.PersonalDatas.SingleOrDefault(x => x.ApplicationUserId == userId) == null)
                return new PersonalData() { ApplicationUserId = userId };

            var personalData = _context.PersonalDatas.Single(x => x.ApplicationUserId == userId);
            return personalData;
        }

        public ICollection<PhoneNumber> GetPhoneNumbers(string userId)
        {
            return _context.PhoneNumbers
                .Include(x => x.PersonalData)
                .Where(x => x.PersonalData.ApplicationUserId == userId)
                .ToList();
        }

        public void AddEditPersonalData(PersonalData personalData, List<string> newPhoneNumbers, List<string> phoneNumbersDoNotDelete)
        {
            List<PhoneNumber> phoneNumbersToAdd = new List<PhoneNumber>();
            if (personalData.Id != 0)
                if (newPhoneNumbers.Any())
                    foreach (var phoneNumber in newPhoneNumbers)
                        phoneNumbersToAdd.Add(new PhoneNumber()
                        {
                            PersonalDataId = personalData.Id,
                            Number = phoneNumber,
                        });

            if(_context.PhoneNumbers.Where(x => x.PersonalDataId == personalData.Id).Any())
                DeletePhoneNumbers(phoneNumbersDoNotDelete, personalData);

            if (_context.PersonalDatas.SingleOrDefault(x => x.ApplicationUserId == personalData.ApplicationUserId) == null)
                AddPersonalData(personalData, newPhoneNumbers);
            else
                EditPersonalData(personalData, phoneNumbersToAdd);
        }

        public void DeletePhoneNumbers(List<string> phoneNumbersDoNotDelete, PersonalData personalData)
        {
            var phoneNumbers = _context.PhoneNumbers.Where(x => x.PersonalDataId == personalData.Id).ToList();
            var phoneNumbersToCompare = new List<PhoneNumber>();

            foreach (var phoneNumber in phoneNumbersDoNotDelete)
                phoneNumbersToCompare.Add(_context.PhoneNumbers.Single(x => x.PersonalDataId == personalData.Id && x.Number == phoneNumber));

            var phoneNumbersToDelete = phoneNumbers.Except(phoneNumbersToCompare).ToList();

            foreach (var phoneNumber in phoneNumbersToDelete)
                _context.PhoneNumbers.Remove(phoneNumber);
        }
        public void EditPersonalData(PersonalData personalData, List<PhoneNumber> phoneNumbersToAdd)
        {
            if (phoneNumbersToAdd.Any())
                foreach (var phoneNumber in phoneNumbersToAdd)
                    _context.PhoneNumbers.Add(phoneNumber);

            var personalDataToEdit = _context.PersonalDatas.Single(x => x.ApplicationUserId == personalData.ApplicationUserId);
            personalDataToEdit.FirstName = personalData.FirstName;
            personalDataToEdit.LastName = personalData.LastName;
            personalDataToEdit.Street = personalData.Street;
            personalDataToEdit.NumberOfStreet = personalData.NumberOfStreet;
            personalDataToEdit.PostalCode = personalData.PostalCode;
            personalDataToEdit.City = personalData.City;
        }

        public void AddPersonalData(PersonalData personalData, List<string> newPhoneNumbers)
        {
            _context.PersonalDatas.Add(personalData);
            _context.SaveChanges();

            List<PhoneNumber> phoneNumbersToAdd = new List<PhoneNumber>();
            if (newPhoneNumbers.Any())
                foreach (var phoneNumber in newPhoneNumbers)
                    phoneNumbersToAdd.Add(new PhoneNumber()
                    {
                        PersonalDataId = personalData.Id,
                        Number = phoneNumber,
                    });

            foreach (var phoneNumber in phoneNumbersToAdd)
                _context.PhoneNumbers.Add(phoneNumber);
        }

        public PersonalData GetPersonalData(int id)
        {
            var announcement = _context.Announcements.Single(x => x.Id == id);
            var personalData = _context.PersonalDatas.Single(x => x.ApplicationUserId == announcement.ApplicationUserId);
            personalData.PhoneNumbers = _context.PhoneNumbers.Where(x => x.PersonalDataId == personalData.Id).ToList();

            return personalData;
        }
    }
}
