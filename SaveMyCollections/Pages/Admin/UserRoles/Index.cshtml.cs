using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Admin.UserRoles
{
    public class IndexModel : PageModel
    {        
        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;    
        }

        public IList<ApplicationUserRole> UserRoles { get; set; } = default!;

        public async Task OnGetAsync()
        {
            UserRoles = new List<ApplicationUserRole>();
            var users = await _userManager.Users.ToListAsync();
            foreach(var user in users)
            {
                var userRole = new ApplicationUserRole();
                userRole.User = user;
                userRole.Roles = await _userManager.GetRolesAsync(user);
                UserRoles.Add(userRole);
            }
        }
        

        
    }
}
