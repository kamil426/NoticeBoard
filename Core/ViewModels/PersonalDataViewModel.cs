using NoticeBoard.Core.Models.Domains;

namespace NoticeBoard.Core.ViewModels
{
    public class PersonalDataViewModel
    {
        public PersonalData PersonalData { get; set; }
        public List<string>? NewPhoneNumbers { get; set; }
    }
}
