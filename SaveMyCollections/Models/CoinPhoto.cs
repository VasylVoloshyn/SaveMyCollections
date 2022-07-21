using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveMyCollections.Models
{
    [Table("CoinPhoto", Schema = "dbo")]
    public class CoinPhoto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CoinId { get; set; }
        public Coin Coin { get; set; } = null!;
        public bool IsAvers { get; set; }
        public bool IsRevers { get; set; }
        [Required]
        public int PhotoId { get; set; }
        public UserPhoto Photo { get; set; } = null!;
    }
}
