using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Coins
{
    public class EditModel : PageModel
    {
        private readonly MyCollectionContext _context;

        public EditModel(MyCollectionContext context)
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
                .Include(b => b.CoinPhotos)
                .ThenInclude(b => b.Photo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coin == null)
            {
                return NotFound();
            }
            Coin = coin;
            foreach (var photo in Coin.CoinPhotos.Select(b => b.Photo))
            {
                photo.PreviewImageUrl = ImageService.GetImageUrl(photo.PreviewImageData);
            }

            ViewData["CoinGradeId"] = new SelectList(_context.CoinGrades, "Id", "Code");
            ViewData["DimeId"] = new SelectList(_context.Dimes, "Id", "Code");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, IFormFile? aversImage, IFormFile? reversImage)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Coin).State = EntityState.Modified;

            var coinToUpdate = await _context.Coins
                .Include(b => b.CoinPhotos)
                .ThenInclude(b => b.Photo)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (coinToUpdate == null)
            {
                return NotFound();
            }

            Coin.CoinPhotos = coinToUpdate.CoinPhotos;
            bool isAversExist = Coin.CoinPhotos.Any(c=>c.IsAvers);
            bool isReversExist = Coin.CoinPhotos.Any(c => c.IsRevers);
            
            List<Photo> photoToRemove = new List<Photo>();
            if (aversImage != null)
            {
                if (isAversExist)
                {
                    photoToRemove.Add(Coin.CoinPhotos.Where(c=>c.IsAvers).First().Photo);
                    Coin.CoinPhotos.Where(c=>c.IsAvers).First().Photo = await ImageService.CreateImageAsync(aversImage);
                }
                else
                {
                    var avers = new CoinPhoto();
                    avers.CoinId = Coin.Id;
                    avers.Photo = await ImageService.CreateImageAsync(aversImage);
                    avers.IsAvers = true;
                    Coin.CoinPhotos.Add(avers);
                }
            }

            if (reversImage != null)
            {
                if (isReversExist)
                {
                    photoToRemove.Add(Coin.CoinPhotos.Where(c=>c.IsRevers).First().Photo);
                    Coin.CoinPhotos.Where(c=>c.IsRevers).First().Photo = await ImageService.CreateImageAsync(reversImage);
                }
                else
                {
                    var revers = new CoinPhoto();
                    revers.CoinId = Coin.Id;
                    revers.Photo = await ImageService.CreateImageAsync(reversImage);
                    revers.IsRevers = true;
                    Coin.CoinPhotos.Add(revers);
                }
            }

            _context.Photos.RemoveRange(photoToRemove);            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoinExists(Coin.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CoinExists(int id)
        {
          return (_context.Coins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
