using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Coins
{
    public class DeleteModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public DeleteModel(MyCollection.Data.MyCollectionContext context)
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

            var coin = await _context.Coins.FirstOrDefaultAsync(m => m.Id == id);

            if (coin == null)
            {
                return NotFound();
            }
            else 
            {
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
                .Include(m => m.CoinPhotos)
                .ThenInclude(m => m.Photo).FirstOrDefaultAsync(i => i.Id == id);

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
