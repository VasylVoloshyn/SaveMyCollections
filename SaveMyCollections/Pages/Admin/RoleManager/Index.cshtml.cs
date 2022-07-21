using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;

namespace SaveMyCollections.Pages.Admin.RoleManager
{
    public class IndexModel : PageModel
    {        
        private readonly RoleManager<IdentityRole> _roleManager;
        public IndexModel( RoleManager<IdentityRole> roleManager)
        {            
            _roleManager = roleManager;
        }

        public IList<IdentityRole> Roles { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_roleManager.Roles != null)
            {
                Roles =  await _roleManager.Roles.ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostAsync(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToPage("./Index");
        }
    }
}
