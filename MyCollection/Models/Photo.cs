using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollection.Models
{
    [Table("Photo", Schema = "dbo")]
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public byte[] ImageData { get; set; } = Array.Empty<byte>();
        [Required]
        public byte[] PreviewImageData { get; set; } = Array.Empty<byte>();
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string FileExtension { get; set; } = string.Empty;
        [Required]
        public decimal Size { get; set; }

        [ValidateNever]
        [FromForm]
        [NotMapped]
        public IFormFile? File { get; set; }

        [ValidateNever]
        [NotMapped]
        public string? ImageUrl { get; set; }

        [ValidateNever]
        [NotMapped]
        public string? PreviewImageUrl { get; set; }

    }
}
