using Microsoft.EntityFrameworkCore;
using WebAnnouncementsApp.Core.Models.Domains;

namespace WebAnnouncementsApp.Core
{
    public interface IApplicationDbContext
    {
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PersonalData> PersonalDatas { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        int SaveChanges();
    }
}
