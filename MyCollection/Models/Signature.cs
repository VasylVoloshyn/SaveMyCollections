using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Models
{
    public class Signature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }
        [ValidateNever]
        public Person Person { get; set; } = null!;
        public string? Note { get; set; } = null!;
    }
}
