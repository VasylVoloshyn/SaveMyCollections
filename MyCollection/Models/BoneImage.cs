using System.ComponentModel.DataAnnotations;

namespace MyCollection.Models
{
    public class BoneImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BoneId { get; set; }
        public Bone Bone { get; set; } = null!;
        [Required]
        public int ImageId { get; set; }
        public Image Image { get; set; } = null!;
    }
}
