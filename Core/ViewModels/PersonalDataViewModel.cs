using WebAnnouncementsApp.Core.Models.Domains;

namespace WebAnnouncementsApp.Core.ViewModels
{
    public class PersonalDataViewModel
    {
        public PersonalData PersonalData { get; set; }
        public List<string>? NewPhoneNumbers { get; set; }
    }
}
