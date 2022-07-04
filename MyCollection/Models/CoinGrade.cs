﻿using System.ComponentModel.DataAnnotations;

namespace MyCollection.Models
{
    public class CoinGrade
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
