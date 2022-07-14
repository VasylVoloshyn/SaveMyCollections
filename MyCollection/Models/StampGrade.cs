using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollection.Models
{
    [Table("StampGrade", Schema = "dbo")]
    public class StampGrade
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = null;
    }
}
