
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Currencies
{
    [Authorize(Roles = "Basic")]
    public class EditModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(MyCollectionContext context, UserManager<ApplicationUser> userManager)
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

            var currency = await _context.Currencies.FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null || currency.User != user)
            {
                return RedirectToPage("/AccessDenied");
            }
            Currency = currency;
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Code");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Currency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(Currency.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CurrencyExists(int id)
        {
          return (_context.Currencies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
