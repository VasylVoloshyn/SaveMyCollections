using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Coins
{
    public class CreateModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public CreateModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CoinGradeId"] = new SelectList(_context.CoinGrades, "Id", "Code");
        ViewData["DimeId"] = new SelectList(_context.Dimes, "Id", "Code");
            return Page();
        }

        [BindProperty]
        public Coin Coin { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile? aversImage, IFormFile? reversImage)
        {
          if (!ModelState.IsValid || _context.Coins == null || Coin == null)
            {
                return Page();
            }
            var coinPhotos = new List<CoinPhoto>();
            if (aversImage != null)
            {
                var avers = new CoinPhoto();
                avers.CoinId = Coin.Id;
                avers.Photo = await ImageService.CreateImageAsync(aversImage);
                coinPhotos.Add(avers);
            }

            if (reversImage != null)
            {
                var revers = new CoinPhoto();
                revers.CoinId = Coin.Id;
                revers.Photo = await ImageService.CreateImageAsync(reversImage);
                coinPhotos.Add(revers);
            }

            Coin.CoinPhotos = coinPhotos;


            _context.Coins.Add(Coin);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
