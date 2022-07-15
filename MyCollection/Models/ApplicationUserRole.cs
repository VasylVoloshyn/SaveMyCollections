using Microsoft.AspNetCore.Identity;

namespace MyCollection.Models
{
    public class ApplicationUserRole
    {
        public ApplicationUser User { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    }
}
