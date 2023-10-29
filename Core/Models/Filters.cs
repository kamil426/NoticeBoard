using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NoticeBoard.Core.Models
{
    public class Filters
    {
        public Filters()
        {
            FiltersSort = new List<FilterSort> 
            {
                new FilterSort() { Id = 1, NameOfFilterSort = "Cena: rosnąco" },
                new FilterSort() { Id = 2, NameOfFilterSort = "Cena: malejąco" },
                new FilterSort() { Id = 3, NameOfFilterSort = "Data: od najnowszych" },
                new FilterSort() { Id = 4, NameOfFilterSort = "Data: od najstarszych" },
            };
        }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int FilterId { get; set; }
        public List<FilterSort> FiltersSort { get; set; }
    }
}
