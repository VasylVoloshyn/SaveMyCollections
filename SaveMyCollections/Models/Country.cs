using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveMyCollections.Models
{
    [Table("Country", Schema = "dbo")]
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "WikiLink")]
        public string? WikiLink { get; set; } = null;
        public ApplicationUser? User { get; set; } = null;
        [NotMapped]
        public bool AllowEdit { get; set; }
    }
}
