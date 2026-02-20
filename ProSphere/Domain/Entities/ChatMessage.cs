using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class ChatMessage
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Sender")]
        public string? SenderId { get; set; }
        public ApplicationUser? Sender { get; set; }

        [ForeignKey("Receiver")]
        public string? ReceiverId { get; set; }
        public ApplicationUser? Receiver { get; set; }

        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? SeenAt { get; set; }
        public bool IsSeen { get; set; }
    }
}
