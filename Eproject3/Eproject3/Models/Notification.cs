namespace Eproject3.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string SendFor { get; set; }
        public string Url { get; set; }
        public bool isRead { get; set; }
    }
}
