using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feedo.Domain
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(500)]
        public string SocialCommentId { get; set; }
        [StringLength(800)]
        public string? OriginalComment { get; set; }
        [StringLength(50)]
        public string? Username { get; set; }
        [StringLength(100)]
        public string? UserFullName { get; set; }
        [StringLength(100)]
        public string Sentiment { get; set; }
        public double SentimentRate { get; set; }
        public int SocialNetworkId { get; set; }
        public SocialNetwork? SocialNetwork { get; set; }
    }
}