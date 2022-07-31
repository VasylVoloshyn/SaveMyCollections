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
        public int CurrencyId { get; set; }
        [ValidateNever]
        public Currency Currency { get; set; } = null!;
        [Required]
        public int Nominal { get; set; }
        public int? Year { get; set; }
        public int? SignatureId { get; set; }
        [ValidateNever]
        public virtual Signature Signature { get; set; } = null!;
        public int GradeID { get; set; }
        [ValidateNever]
        public virtual BanknoteGrade Grade { get; set; } = null!;
        public double? Price { get; set; } = null!;
        public string? Note { get; set; } = null!;
        [ValidateNever]
        public ICollection<BanknotePhoto> BanknotePhoto { get; set; } = null!;
        public ApplicationUser? User { get; set; } = null;
        [NotMapped]
        public bool AllowEdit { get; set; }
    }
}
