using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollection.Models
{
    [Table("Country", Schema = "dbo")]
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;
        public string? WikiLink { get; set; } = null;
        public ApplicationUser? User { get; set; } = null;
        [NotMapped]
        public bool AllowEdit { get; set; }
    }
}
