using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class Conversation
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public Creator Creator { get; set; }

        [ForeignKey("Investor")]
        public string InvestorId { get; set; }
        public Investor Investor { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageSentAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
