using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Coins
{
    public class DeleteModel : PageModel
    {
        private readonly MyCollectionContext _context;

        public DeleteModel(MyCollectionContext context)
        {
            _context = context;
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
                Coin = coin;
                foreach (var photo in Coin.CoinPhotos.Select(b => b.Photo))
                {
                    photo.PreviewImageUrl = ImageService.GetImageUrl(photo.PreviewImageData);
                }
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
                .Include(m => m.CoinPhotos)
                .ThenInclude(m => m.Photo)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (coin != null)
            {
                Coin = coin;
                _context.Coins.Remove(Coin);
                var photos = Coin.CoinPhotos.Select(o => o.Photo);
                foreach (var photo in photos)
                {
                    _context.Photos.Remove(photo);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
