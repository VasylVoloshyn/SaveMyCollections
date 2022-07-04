using System.ComponentModel.DataAnnotations;

namespace MyCollection.Models
{
    public class CoinPhoto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CoinId { get; set; }
        public Coin Coin { get; set; } = null!;
        [Required]
        public int PhotoId { get; set; }
        public Photo Photo { get; set; } = null!;
    }
}
