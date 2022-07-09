using Microsoft.EntityFrameworkCore;
using MyCollection.Models;

namespace MyCollection.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MyCollectionContext context)
        {
            context.Database.Migrate();
            if (context.BoneGrades.Any())
            {
                return;   // DB has been seeded
            }

            
            Grade crade = new Grade
            {
                Code = "UNC",
                Name = "Uncirculated",
                Description = "test"
            };
            context.BoneGrades.Add(crade);
            context.SaveChanges();

        }
    }
}
