using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;
using SaveMyCollectionsService;

namespace SaveMyCollections.Pages.Coins
{
    [Authorize(Roles = "Basic")]
    public class DeleteModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(SaveMyCollectionsContext context, IWebHostEnvironment hostingEnv,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }

        [BindProperty]
        public Coin Coin { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Coins == null)
            {
                return NotFound();
            }

            var coin = await _context.Coins
                .Include(c => c.User)
                .Include(c => c.Dime)
                .Include(c => c.CoinGrade)
                .Include(c => c.CoinPhotos)
                .ThenInclude(c => c.Photo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (coin == null)
            {
                return NotFound();
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || coin.User?.Id != user.Id)
                {
                    return RedirectToPage("/AccessDenied");
                }
                Coin = coin;                
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Coins == null)
            {
                return NotFound();
            }

            var coin = await _context.Coins
                .Include(c => c.User)
                .Include(c => c.CoinPhotos)
                .ThenInclude(c => c.Photo)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (coin != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || coin.User?.Id != user.Id)
                {
                    return RedirectToPage("/AccessDenied");
                }
                Coin = coin;
                _context.Coins.Remove(Coin);
                var photos = Coin.CoinPhotos.Select(o => o.Photo);
                foreach (var photo in photos)
                {
                    await UserPhotoServise.DeletePhotoAsync(_hostingEnv, photo);
                    _context.UserPhotos.Remove(photo);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
