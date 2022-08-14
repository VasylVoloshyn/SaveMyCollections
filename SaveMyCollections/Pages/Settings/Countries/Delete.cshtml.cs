
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Settings.Countries
{
    [Authorize(Roles = "Basic")]
    public class DeleteModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(SaveMyCollectionsContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
      public Country Country { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries                
                .FirstOrDefaultAsync(m => m.Id == id);

            if (country == null)
            {
                return NotFound();
            }
            else 
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || country.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                Country = country;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }
            var country = await _context.Countries.FindAsync(id);

            if (country != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || country.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                Country = country;
                _context.Countries.Remove(Country);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
