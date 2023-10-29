using Microsoft.EntityFrameworkCore;
using NoticeBoard.Core.Models.Domains;

namespace NoticeBoard.Core.Repository
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetCategories();
    }
}
