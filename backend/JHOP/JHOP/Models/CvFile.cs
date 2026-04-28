using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JHOP.Models
{
    public class CvFile
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey(nameof(UserId))]
       
        public AppUser User { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }
        [Required]
        public int OfferId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string FileName { get; set; }

        public string Sha256 { get; set; } = "";

        public long SizeBytes { get; set; }

        [Required]
        public byte[] Data { get; set; } = Array.Empty<byte>();

        [Required]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;


    }
}
