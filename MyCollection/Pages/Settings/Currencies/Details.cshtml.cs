using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Currencies
{
    public class DetailsModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(MyCollectionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

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
                if (user != null)
                {
                    if (currency.User?.Id == user.Id)
                    {
                        currency.AllowEdit = true;
                    }
                }
                Currency = currency;
            }
            return Page();
        }
    }
}
