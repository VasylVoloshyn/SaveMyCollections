using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveMyCollections.Models
{
    [Table("Person", Schema = "dbo")]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string FamilyName { get; set; } = string.Empty;
        public string? FatherName { get; set; } = string.Empty;
        public string? Note { get; set; } = null!;
        public string? WikiLink { get; set; } = null!;

        public ApplicationUser? User { get; set; } = null;
        [NotMapped]
        public bool AllowEdit { get; set; }
    }
}
