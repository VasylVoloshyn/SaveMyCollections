using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveMyCollections.Models
{
    [Table("Dime", Schema = "dbo")]
    public class Dime
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [ValidateNever]
        [Display(Name = "Country")]
        public Country Country { get; set; } = null!;
        [Display(Name = "WikiLink")]
        public string? WikiLink { get; set; } = null!;
        public ApplicationUser? User { get; set; } = null;
        [NotMapped]
        public bool AllowEdit { get; set; }
    }
}
