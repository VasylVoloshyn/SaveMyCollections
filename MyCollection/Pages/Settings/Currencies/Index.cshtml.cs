using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Currencies
{
    public class IndexModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(MyCollectionContext context, UserManager<ApplicationUser> userManager)
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
