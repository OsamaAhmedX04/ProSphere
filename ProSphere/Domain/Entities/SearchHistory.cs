using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSphere.Domain.Entities
{
    public class SearchHistory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string? SearchTerm { get; set; }
        public string FullSearchTerm { get; set; }
        public string SearchCategory { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
