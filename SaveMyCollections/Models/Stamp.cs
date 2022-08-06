using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveMyCollections.Models
{
    [Table("Stamp", Schema = "dbo")]
    public class Stamp
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [ValidateNever]
        [Display(Name = "Country")]
        public Country? Country { get; set; }
        [Display(Name = "Currency")]
        public int? CurrencyId { get; set; }
        [ValidateNever]
        [Display(Name = "Currency")]
        public Currency? Currency { get; set; }
        [Display(Name = "Dime")]
        public int ? DimeId { get; set; }
        [ValidateNever]
        [Display(Name = "Dime")]
        public Dime? Dime { get; set; }
        [Display(Name = "Nominal")]
        public int? Nominal { get; set; }
        [Display(Name = "Year")]
        public int? Year { get; set; }
        [Display(Name = "IsCancelated")]
        public bool IsCancelated { get; set; }
        [Display(Name = "Grade")]
        public int StampGradeId { get; set; }
        [ValidateNever]
        [Display(Name = "Grade")]
        public StampGrade StampGrade { get; set; } = null!;
        [Display(Name = "Price")]
        public double? Price { get; set; } = null!;
        [Display(Name = "Note")]
        public string? Note { get; set; } = null!;
        [Display(Name = "Photo")]
        public int? StampPhotoId { get; set; }
        [ValidateNever]
        [Display(Name = "Photo")]
        public virtual UserPhoto? StampPhoto { get; set; } = null!;
        public ApplicationUser? User { get; set; } = null;
        [NotMapped]
        public bool AllowEdit { get; set; }
    }
}
