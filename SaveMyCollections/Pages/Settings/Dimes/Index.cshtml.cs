using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Settings.Dimes
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

        public IList<Dime> Dime { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Dimes != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user?.Id;
                Dime = await _context.Dimes
                    .Where(d => d.User == null || d.User.Id == userId )
                    .Include(d => d.Country).ToListAsync();

                if (user != null)
                {
                    foreach (var dime in Dime)
                    {
                        if (dime.User?.Id == user.Id)
                        {
                            dime.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
