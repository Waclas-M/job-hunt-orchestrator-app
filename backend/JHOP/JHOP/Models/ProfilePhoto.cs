using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JHOP.Models
{
    public class ProfilePhoto
    {

        public int Id { get; set; }
        public int ProfileId { get; set; }
        [JsonIgnore]
        public Profile Profile { get; set; } = default!;
        [JsonIgnore]
        public string Sha256 { get; set; } = "";
        [JsonIgnore]
        public long SizeBytes { get; set; }

        [Required]
        public byte[] Data { get; set; } = Array.Empty<byte>();

        [Required]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
