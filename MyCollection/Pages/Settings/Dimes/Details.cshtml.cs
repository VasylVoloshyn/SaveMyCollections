using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Dimes
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

      public Dime Dime { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Dimes == null)
            {
                return NotFound();
            }

            var dime = await _context.Dimes
                .Include(d => d.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dime == null)
            {
                return NotFound();
            }
            else 
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    if (dime.User?.Id == user.Id)
                    {
                        dime.AllowEdit = true;
                    }
                }
                Dime = dime;
            }
            return Page();
        }
    }
}
