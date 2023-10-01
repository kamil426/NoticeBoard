using WebAnnouncementsApp.Core.Models.Domains;

namespace WebAnnouncementsApp.Core.Service
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetCategories();
    }
}
