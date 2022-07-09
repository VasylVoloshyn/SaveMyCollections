using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Models
{
    public class Stamp
    {
        [Key]
        public int Id { get; set; }        
        public int CountryId { get; set; }
        [ValidateNever]
        public Country? Country { get; set; }            
        public int? CurrencyId { get; set; }
        [ValidateNever]
        public Currency? Currency { get; set; }
        public int ? DimeId { get; set; }
        [ValidateNever]
        public Dime? Dime { get; set; }
        public int? Nominal { get; set; }
        public int? Year { get; set; }
        public bool IsCancelated { get; set; }
        public int StampGradeId { get; set; }
        [ValidateNever]
        public StampGrade StampGrade { get; set; } = null!;
        public double? Price { get; set; } = null!;
        public string? Note { get; set; } = null!;
        public int? StampPhotoId { get; set; }
        [ValidateNever]
        public virtual Photo? StampPhoto { get; set; } = null!;
    }
}
