using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Dimes
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

        public IList<Dime> Dime { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Dimes != null)
            {
                var user = await _userManager.GetUserAsync(User);
                Dime = await _context.Dimes
                    .Where(d => d.User == user || d.User == null)
                    .Include(d => d.Country).ToListAsync();

                if (user != null)
                {
                    foreach (var dime in Dime)
                    {
                        if (dime.User == user)
                        {
                            dime.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
