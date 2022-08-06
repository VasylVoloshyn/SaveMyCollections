using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveMyCollections.Models
{    
    [Table("Banknote",Schema = "dbo")]
    public class Banknote
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        [ValidateNever]
        [Display(Name = "Currency")]
        public Currency Currency { get; set; } = null!;
        [Required]
        [Display(Name = "Nominal")]
        public int Nominal { get; set; }
        [Display(Name = "Year")]
        public int? Year { get; set; }
        [Display(Name = "Signature")]
        public int? SignatureId { get; set; }
        [ValidateNever]
        [Display(Name = "Signature")]
        public virtual Signature Signature { get; set; } = null!;
        [Display(Name = "Grade")]
        public int GradeID { get; set; }
        [ValidateNever]
        [Display(Name = "Grade")]
        public virtual BanknoteGrade Grade { get; set; } = null!;
        [Display(Name = "Price")]
        public double? Price { get; set; } = null!;
        [Display(Name = "Note")]
        public string? Note { get; set; } = null!;
        [ValidateNever]
        public ICollection<BanknotePhoto> BanknotePhoto { get; set; } = null!;
        public ApplicationUser? User { get; set; } = null;
        [NotMapped]
        public bool AllowEdit { get; set; }
    }
}
