using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveMyCollections.Models
{
    [Table("Coin", Schema = "dbo")]
    public class Coin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Dime")]
        public int DimeId { get; set; }
        [ValidateNever]
        [Display(Name = "Dime")]
        public Dime Dime { get; set; } = null!;
        [Required]
        [Display(Name = "Nominal")]
        public int Nominal { get; set; }
        [Display(Name = "Year")]
        public int? Year { get; set; }
        [Display(Name = "Grade")]
        public int CoinGradeId { get; set; }
        [ValidateNever]
        [Display(Name = "Grade")]
        public virtual CoinGrade CoinGrade { get; set; } = null!;
        [Display(Name = "Price")]
        public double? Price { get; set; } = null!;
        [Display(Name = "Commemorative")]
        public bool Commemorative { get; set; } = false;

        [Display(Name = "FirstDate")]
        public DateTime? FirstDate { get; set; } = null!;

        [Display(Name = "Name")]
        public string? Name { get; set; } = null!;

        [Display(Name = "Note")]
        public string? Note { get; set; } = null!;

        [Display(Name = "Description")]
        public string? Description { get; set; } = null!;

        [Display(Name = "Material")]
        public int? CoinMaterialId { get; set; }
        [ValidateNever]
        [Display(Name = "Material")]
        public virtual Material CoinMaterial { get; set; } = null!;

        [ValidateNever]
        public ICollection<CoinPhoto> CoinPhotos { get; set; } = null!;
        public ApplicationUser? User { get; set; } = null;
        [NotMapped]
        public bool AllowEdit { get; set; }
    }
}
