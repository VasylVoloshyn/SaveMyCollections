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
        public int DimeId { get; set; }
        [ValidateNever]
        public Dime Dime { get; set; } = null!;
        [Required]
        public int Nominal { get; set; }
        public int? Year { get; set; }       
        public int CoinGradeId { get; set; }
        [ValidateNever]
        public virtual CoinGrade CoinGrade { get; set; } = null!;
        public double? Price { get; set; } = null!;
        public string? Note { get; set; } = null!;
        [ValidateNever]
        public ICollection<CoinPhoto> CoinPhotos { get; set; } = null!;
        public ApplicationUser? User { get; set; } = null;
        [NotMapped]
        public bool AllowEdit { get; set; }
    }
}
