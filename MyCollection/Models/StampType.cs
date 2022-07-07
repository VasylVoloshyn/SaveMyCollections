using System.ComponentModel.DataAnnotations;

namespace MyCollection.Models
{
    public class StampType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

    }
}
