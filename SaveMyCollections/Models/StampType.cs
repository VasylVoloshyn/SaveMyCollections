using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveMyCollections.Models
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
        public ApplicationUser? User { get; set; } = null;
        [NotMapped]
        public bool AllowEdit { get; set; }

    }
}
