using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoticeBoard.Core.Models.Domains
{
    public class Announcement
    {
        public Announcement()
        {
            Images = new Collection<Image>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Pole 'Tytuł' jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        public string? Description { get; set; }

        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        public DateTime DateOfCreate { get; set; }
        public int TitleImageId { get; set; }

        [ForeignKey("Category")]
        [Required(ErrorMessage = "Pole 'Kategoria' jest wymagane")]
        [Display(Name = "Kategorie")]
        public int CategoryId { get; set; }

        [ForeignKey("User")]
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public Category? Category { get; set; }

        [Display(Name = "Zdjęcia")]
        public ICollection<Image> Images { get; set; }
    }
}
