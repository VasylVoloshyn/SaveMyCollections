using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollection.Models
{
    [Table("StampType", Schema = "dbo")]
    public class StampType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? WikiLink  { get; set; } = null;

    }
}
