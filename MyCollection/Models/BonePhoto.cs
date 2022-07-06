using System.ComponentModel.DataAnnotations;

namespace MyCollection.Models
{
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
