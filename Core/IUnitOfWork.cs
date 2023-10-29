using NoticeBoard.Core.Repository;
using NoticeBoard.Persistence.Repository;

namespace NoticeBoard.Core
{
    public interface IUnitOfWork
    {
        IAnnouncementRepository Announcement { get; set; }
        ICategoryRepository Category { get; set; }
        IPersonalDataRepository PersonalData { get; set; }
        void Complete();
    }
}
