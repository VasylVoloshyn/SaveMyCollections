using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCollection.Models;


namespace MyCollection.Data
{
    public class MyCollectionContext : IdentityDbContext<ApplicationUser>
    {
        #region General        
        public DbSet<Photo> Photos { get; set; } = null!;
        public DbSet<UserPhoto> UserPhotos { get; set; } = null!;
        public DbSet<Currency> Currencies { get; set; } = null!;
        public DbSet<Dime> Dimes { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;

        #endregion General

        #region Bones
        public DbSet<Signature> Signatures { get; set; } = null!;
        public DbSet<BoneGrade> BoneGrades { get; set; } = null!;
        public DbSet<Bone> Bones { get; set; } = null!;
        public DbSet<BonePhoto> BonePhotos { get; set; } = null!;
        #endregion Bones

        #region Coins
        public DbSet<CoinPhoto> CoinPhotos { get; set; } = null!;
        public DbSet<CoinGrade> CoinGrades { get; set; } = null!;
        public DbSet<Coin> Coins { get; set; } = null!;
        #endregion Coins

        #region Stamps
        public DbSet<StampType> StampTypes { get; set; } = null!;
        public DbSet<StampGrade> StampGrades { get; set; } = null!;
        public DbSet<Stamp> Stamps { get; set; } = null!;
        #endregion Stamps

        public MyCollectionContext(DbContextOptions<MyCollectionContext> option) : base(option) { }            

    }
}
