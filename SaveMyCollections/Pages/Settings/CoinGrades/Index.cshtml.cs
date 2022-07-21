using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.CoinGrades
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

        public IList<CoinGrade> CoinGrade { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CoinGrades != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user?.Id;
                CoinGrade = await _context.CoinGrades
                    .Where(c => c.User == null || c.User.Id == userId)
                    .ToListAsync();

                if (user != null)
                {
                    foreach (var coinGrade in CoinGrade)
                    {
                        if (coinGrade.User?.Id == user.Id)
                        {
                            coinGrade.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
