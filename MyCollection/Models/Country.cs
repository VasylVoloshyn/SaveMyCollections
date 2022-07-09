using System.ComponentModel.DataAnnotations;

namespace MyCollection.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;
        public string? WikiLink { get; set; } = null;
    }
}
