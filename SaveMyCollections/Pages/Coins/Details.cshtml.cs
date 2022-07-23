using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;
using SaveMyCollections.Services;

namespace SaveMyCollections.Pages.Coins
{
    public class DetailsModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(SaveMyCollectionsContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;            
        }

        public Coin Coin { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Coins == null)
            {
                return NotFound();
            }

            var coin = await _context.Coins
                .Include(c => c.User)
                .Include(c => c.CoinPhotos)
                .ThenInclude(c => c.Photo)
                .Include(c => c.CoinGrade)
                .Include(c => c.Dime)
                .ThenInclude(b => b.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coin == null)
            {
                return NotFound();
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    if (coin.User?.Id == user.Id)
                    {
                        coin.AllowEdit = true;
                    }
                }
                Coin = coin;                
            }

            return Page();
        }
    }
}
