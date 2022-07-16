using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Currencies
{
    [Authorize(Roles = "Basic")]
    public class CreateModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(MyCollectionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Code");
            return Page();
        }

        [BindProperty]
        public Currency Currency { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Currencies == null || Currency == null)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            Currency.User = user;
            _context.Currencies.Add(Currency);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
