using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Models;

namespace SaveMyCollections.Data
{
    public class SaveMyCollectionsContext : IdentityDbContext<ApplicationUser>
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
        public DbSet<BanknoteGrade> BanknoteGrades { get; set; } = null!;
        public DbSet<Banknote> Banknotes { get; set; } = null!;
        public DbSet<BanknotePhoto> BonePhotos { get; set; } = null!;
        #endregion Bones

        #region Coins
        public DbSet<CoinPhoto> CoinPhotos { get; set; } = null!;
        public DbSet<CoinGrade> CoinGrades { get; set; } = null!;
        public DbSet<Coin> Coins { get; set; } = null!;
        public DbSet<Material> Materials { get;set; } = null!;
        #endregion Coins

        #region Stamps
        public DbSet<StampType> StampTypes { get; set; } = null!;
        public DbSet<StampGrade> StampGrades { get; set; } = null!;
        public DbSet<Stamp> Stamps { get; set; } = null!;
        #endregion Stamps

        public SaveMyCollectionsContext(DbContextOptions<SaveMyCollectionsContext> option) : base(option) { }            

    }
}
