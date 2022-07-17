using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.CoinGrades
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

        public IList<CoinGrade> CoinGrade { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CoinGrades != null)
            {
                var user = await _userManager.GetUserAsync(User);
                CoinGrade = await _context.CoinGrades
                    .Where(c => c.User == user || c.User == null)
                    .ToListAsync();

                if (user != null)
                {
                    foreach (var coinGrade in CoinGrade)
                    {
                        if (coinGrade.User == user)
                        {
                            coinGrade.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
