using NoticeBoard.Core;
using NoticeBoard.Core.Models.Domains;
using NoticeBoard.Core.Repository;

namespace NoticeBoard.Persistence.Repository
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
