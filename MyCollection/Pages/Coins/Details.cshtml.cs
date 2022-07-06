using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Coins
{
    public class DetailsModel : PageModel
    {
        private readonly MyCollectionContext _context;

        public DetailsModel(MyCollectionContext context)
        {
            _context = context;
        }

        public Coin Coin { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Coins == null)
            {
                return NotFound();
            }

            var coin = await _context.Coins
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
                Coin = coin;
                foreach (var photo in Coin.CoinPhotos.Select(c => c.Photo))
                {
                    photo.ImageUrl = ImageService.GetImageUrl(photo.ImageData);
                }
            }

            return Page();
        }
    }
}
