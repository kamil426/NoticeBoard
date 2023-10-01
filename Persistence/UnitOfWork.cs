using WebAnnouncementsApp.Core;
using WebAnnouncementsApp.Core.Repository;
using WebAnnouncementsApp.Persistence.Repository;

namespace WebAnnouncementsApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;

        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            Announcement = new AnnouncementRepository(context);
            Category = new CategoryRepository(context);
            PersonalData = new PersonalDataRepository(context);
        }
        public IAnnouncementRepository Announcement { get; set; }
        public ICategoryRepository Category { get; set; }
        public IPersonalDataRepository PersonalData { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
