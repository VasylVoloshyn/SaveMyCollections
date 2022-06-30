using Microsoft.EntityFrameworkCore;
using MyCollection.Models;

namespace MyCollection.Data
{
    public class MyCollectionContext : DbContext
    {
        public DbSet<Signature> Signatures { get; set; } = null;
        public DbSet<Image> Images { get; set; } = null;
        public DbSet<Grade> Grades { get; set; } = null;
        public DbSet<Country> Countries { get; set; } = null;
        public DbSet<Bone> Bones { get; set; } = null;

        public MyCollectionContext(DbContextOptions<MyCollectionContext> option) : base(option) { }

    }
}
