using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; }
        public long ZoomMeetingId { get; set; }

        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public Creator Creator { get; set; }

        [ForeignKey("Investor")]
        public string InvestorId { get; set; }
        public Investor Investor { get; set; }

        public string JoinUrl { get; set; }
        public string StartUrl { get; set; }
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
