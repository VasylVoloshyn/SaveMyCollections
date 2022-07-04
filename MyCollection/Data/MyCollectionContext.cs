using Microsoft.EntityFrameworkCore;
using MyCollection.Models;


namespace MyCollection.Data
{
    public class MyCollectionContext : DbContext
    {
        public DbSet<Signature> Signatures { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; } = null!;
        public DbSet<Grade> Grades { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Bone> Bones { get; set; } = null!;
       
        public DbSet<Person>? Person { get; set; }

        public DbSet<Currency>? Currency { get; set; }

        public DbSet<Dime> Dimes { get; set; }

        public DbSet<CoinPhoto> CoinPhotos { get; set; }

        public DbSet<CoinGrade> CoinGrades { get; set; }

        public DbSet<Coin> Coins { get; set; }

        public MyCollectionContext(DbContextOptions<MyCollectionContext> option) : base(option) { }


    }
}
