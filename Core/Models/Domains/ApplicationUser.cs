using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAnnouncementsApp.Core.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Announcements = new Collection<Announcement>();
        }
        public PersonalData? PersonalData { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
    }
}
