using System.ComponentModel.DataAnnotations;

namespace ProSphere.Domain.Entities
{
    public class ChatMessageHistory
    {
        [Key]
        public int Id { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string? MessageContent { get; set; }
        public string? ImageContent { get; set; }
        public DateTime SentAt { get; set; }
    }
}
