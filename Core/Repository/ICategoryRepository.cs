using Microsoft.EntityFrameworkCore;
using WebAnnouncementsApp.Core.Models.Domains;

namespace WebAnnouncementsApp.Core.Repository
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetCategories();
    }
}
