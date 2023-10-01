using System.ComponentModel.DataAnnotations.Schema;

namespace WebAnnouncementsApp.Core.Models.Domains
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; } 

        [ForeignKey("Announcement")]
        public int AnnouncementId { get; set; }
        public Announcement? Announcement { get; set; }
    }
}
