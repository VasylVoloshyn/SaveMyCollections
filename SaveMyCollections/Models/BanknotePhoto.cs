using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveMyCollections.Models
{
    [Table("BanknotePhoto", Schema = "dbo")]
    public class BanknotePhoto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BanknoteId { get; set; }
        public Banknote Banknote { get; set; } = null!;
        [Required]
        public bool IsAvers { get; set; }
        public bool IsRevers { get; set; }
        public int PhotoId { get; set; }
        public UserPhoto Photo { get; set; } = null!;
    }
}
