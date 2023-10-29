namespace NoticeBoard.Core.Models.Domains
{
    public class Category
    {
        public Category()
        {
            Announcements = new List<Announcement>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
    }
}
