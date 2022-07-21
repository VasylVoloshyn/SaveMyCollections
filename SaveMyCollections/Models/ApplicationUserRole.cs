using Microsoft.AspNetCore.Identity;

namespace SaveMyCollections.Models
{
    public class ApplicationUserRole
    {
        public ApplicationUser User { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    }
}
