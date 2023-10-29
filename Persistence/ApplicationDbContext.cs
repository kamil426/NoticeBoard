using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoticeBoard.Core;
using NoticeBoard.Core.Models.Domains;

namespace NoticeBoard.Persistence
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PersonalData> PersonalDatas { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    }

}