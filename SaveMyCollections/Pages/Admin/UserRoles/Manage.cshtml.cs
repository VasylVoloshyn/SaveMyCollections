using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Admin.UserRoles
{
    [Authorize(Roles = "SuperAdmin")]
    public class ManageModel : PageModel
    {
        private readonly SaveMyCollections.Data.SaveMyCollectionsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ManageModel(SaveMyCollections.Data.SaveMyCollectionsContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }
        [BindProperty]
        public List<ManageUserRoles> ManageUserRoles { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            ManageUserRoles = new List<ManageUserRoles>();
            var userId = id.ToString();
            ViewData["userId"] = userId;
            
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewData["ErrorMessage"] = $"User with Id = {userId} cannot be found";
                return NotFound();
            }
            ViewData["UserName"] = user.UserName;

            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new ManageUserRoles
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                ManageUserRoles.Add(userRolesViewModel);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = id.ToString();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Page();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return Page();
            }
            result = await _userManager.AddToRolesAsync(user, ManageUserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return Page();
            }



            return RedirectToPage("./Index");
        }
    }
}
