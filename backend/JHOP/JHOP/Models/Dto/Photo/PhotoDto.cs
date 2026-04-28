namespace JHOP.Models.Dto.Photo
{
    public class PhotoDto
    {
        public int ProfileId { get; set; }
        public IFormFile selectedFile { get; set; } = default!;
    }
}
