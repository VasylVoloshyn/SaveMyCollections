using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SaveMyCollections.Models
{
    [Table("UserPhoto", Schema = "dbo")]
    public class UserPhoto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; } = string.Empty;
        [Required]
        public string FileExtension { get; set; } = string.Empty;
        [Required]
        public string FileLocation { get; set; } = string.Empty;
        [Required]
        public decimal Size { get; set; }
                
        [ValidateNever]
        [FromForm]
        [NotMapped]
        public IFormFile? File { get; set; }
        
    }
}
