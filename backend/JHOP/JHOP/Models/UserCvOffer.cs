using JHOP.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JHOP.Models
{
    public class UserCvOffer
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string OfferURL { get; set; }

        
        [Column(TypeName = "nvarchar(450)")]
        public string? CompanyName { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public CvOfferStatus Status { get; set; }


        public int CvFileId { get; set; }





    }
}
