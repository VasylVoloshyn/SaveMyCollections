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
    public class DetailsModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public DetailsModel(MyCollection.Data.MyCollectionContext context)
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
    }
}
