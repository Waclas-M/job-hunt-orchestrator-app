using JHOP.Enums;
using System.ComponentModel.DataAnnotations;

namespace JHOP.Models
{
    public class LaborMarketOffer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string JobTitle { get; set; }
        
        public string? Description { get; set; } 
        [Required]
        public string Location { get; set; }
        [Required]
        public List<string> Technologies { get; set; }
        [Required]
        public List<string> Requirements { get; set; }
        [Required]
        public List<string> Responsibilities { get; set; }
        [Required]
        public LaborOfferCategorie Categorie { get; set; }

        [Required]
        public string Url { get; set; }


    }
}
