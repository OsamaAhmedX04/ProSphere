using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Conversation")]
        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }


        [ForeignKey("Sender")]
        public string? SenderId { get; set; }
        public ApplicationUser? Sender { get; set; }

        [ForeignKey("Receiver")]
        public string? ReceiverId { get; set; }
        public ApplicationUser? Receiver { get; set; }

        public string? Content { get; set; }
        public string? ImageURL { get; set; }
        public DateTime SentAt { get; set; }

    }
}
