using System.ComponentModel.DataAnnotations;

namespace MyCollection.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string FamilyName { get; set; } = string.Empty;
        public string? Note { get; set; } = null!;
        public string? WikiLink { get; set; } = null!;
    }
}
