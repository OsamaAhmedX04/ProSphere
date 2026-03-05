namespace ProSphere.Shared.DTOs.Notification
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? SeenAt { get; set; }
        public bool IsSeen { get; set; }
    }
}
