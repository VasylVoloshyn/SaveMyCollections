using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Settings.Countries
{
    public class IndexModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(SaveMyCollectionsContext context,
            UserManager<ApplicationUser> userManager)            
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Country> Country { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Countries != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user?.Id;
                Country = await _context.Countries
                    .Where(c => c.User == null || c.User.Id == userId)
                    .ToListAsync();

                if (user != null)
                {
                    foreach (var country in Country)
                    {
                        if (country.User?.Id == user.Id)
                        {
                            country.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
