using NoticeBoard.Core;
using NoticeBoard.Core.Models.Domains;
using NoticeBoard.Core.Service;

namespace NoticeBoard.Persistence.Services
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
