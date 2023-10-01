using WebAnnouncementsApp.Core;
using WebAnnouncementsApp.Core.Models.Domains;
using WebAnnouncementsApp.Core.Repository;

namespace WebAnnouncementsApp.Persistence.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        private IApplicationDbContext _context;

        public CategoryRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
