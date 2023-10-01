using WebAnnouncementsApp.Core.Repository;
using WebAnnouncementsApp.Persistence.Repository;

namespace WebAnnouncementsApp.Core
{
    public interface IUnitOfWork
    {
        IAnnouncementRepository Announcement { get; set; }
        ICategoryRepository Category { get; set; }
        IPersonalDataRepository PersonalData { get; set; }
        void Complete();
    }
}
