using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Settings.Countries
{
    [Authorize(Roles = "Basic")]
    public class CreateModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(SaveMyCollectionsContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Country Country { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Countries == null || Country == null)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            Country.User = user;
            _context.Countries.Add(Country);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
