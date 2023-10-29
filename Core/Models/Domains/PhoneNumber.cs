using System.ComponentModel.DataAnnotations.Schema;

namespace NoticeBoard.Core.Models.Domains
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }

        [ForeignKey("PersonalData")]
        public int PersonalDataId { get; set; }
        public PersonalData? PersonalData { get; set; }
    }
}
