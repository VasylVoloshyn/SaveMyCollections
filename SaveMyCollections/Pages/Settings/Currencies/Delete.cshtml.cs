using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Settings.Currencies
{
    [Authorize(Roles = "Basic")]
    public class DeleteModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(SaveMyCollectionsContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
      public Currency Currency { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Currencies == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (currency == null)
            {
                return NotFound();
            }
            else 
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || currency.User?.Id != user.Id)
                {
                    return RedirectToPage("/AccessDenied");
                }
                Currency = currency;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Currencies == null)
            {
                return NotFound();
            }
            var currency = await _context.Currencies.FindAsync(id);

            if (currency != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || currency.User?.Id != user.Id)
                {
                    return RedirectToPage("/AccessDenied");
                }
                Currency = currency;
                _context.Currencies.Remove(Currency);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
