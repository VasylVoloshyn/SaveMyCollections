using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Settings.Currencies
{
    public class IndexModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(SaveMyCollectionsContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Currency> Currency { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Currencies != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user?.Id;
                Currency = await _context.Currencies
                    .Where(c => c.User == null || c.User.Id == userId)
                    .Include(c => c.Country).ToListAsync();
                if (user != null)
                {
                    foreach (var currency in Currency)
                    {
                        if (currency.User?.Id == user.Id)
                        {
                            currency.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
