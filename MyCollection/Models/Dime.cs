using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollection.Models
{
    [Table("Dime", Schema = "dbo")]
    public class Dime
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int CountryId { get; set; }
        [ValidateNever]
        public Country Country { get; set; } = null!;
        public string? WikiLink { get; set; } = null!;
    }
}
