using NoticeBoard.Core.Models.Domains;

namespace NoticeBoard.Core.Service
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetCategories();
    }
}
