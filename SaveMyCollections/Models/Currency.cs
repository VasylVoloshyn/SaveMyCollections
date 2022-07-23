using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveMyCollections.Models
{
    [Table("Currency", Schema = "dbo")]
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name="Country")]
        public int CountryId { get; set; }
        [ValidateNever]
        [Display(Name = "Country")]
        public Country Country { get; set; } = null!;
        public string? WikiLink { get; set; } = null!;
        public ApplicationUser? User { get; set; } = null;
        [NotMapped]
        public bool AllowEdit { get; set; }
    }
}
