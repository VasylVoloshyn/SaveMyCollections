using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollection.Models
{
    [Table("BonePhoto", Schema = "dbo")]
    public class BonePhoto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BoneId { get; set; }
        public Bone Bone { get; set; } = null!;
        [Required]
        public bool IsAvers { get; set; }
        public bool IsRevers { get; set; }
        public int PhotoId { get; set; }
        public Photo Photo { get; set; } = null!;
    }
}
