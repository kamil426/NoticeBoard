using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace NoticeBoard.Core.Models.Domains
{
    public class PersonalData
    {
        public PersonalData()
        {
            PhoneNumbers = new Collection<PhoneNumber>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole 'Imię' jest wymagane")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole 'Nazwisko' jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Ulica")]
        [Required(ErrorMessage = "Pole 'Ulica' jest wymagane")]
        public string Street { get; set; }

        [Display(Name = "Numer ulicy")]
        [Required(ErrorMessage = "Pole 'Numer ulicy' jest wymagane")]
        public string NumberOfStreet { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Pole 'Miasto' jest wymagane")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "Pole 'Kod pocztowy' jest wymagane")]
        public string PostalCode { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
