using WebAnnouncementsApp.Core;
using WebAnnouncementsApp.Core.Models.Domains;
using WebAnnouncementsApp.Core.Service;

namespace WebAnnouncementsApp.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Category.GetCategories();
        }
    }
}
