using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.CoinGrades
{
    public class DeleteModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public DeleteModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CoinGrade CoinGrade { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CoinGrades == null)
            {
                return NotFound();
            }

            var coingrade = await _context.CoinGrades.FirstOrDefaultAsync(m => m.Id == id);

            if (coingrade == null)
            {
                return NotFound();
            }
            else 
            {
                CoinGrade = coingrade;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CoinGrades == null)
            {
                return NotFound();
            }
            var coingrade = await _context.CoinGrades.FindAsync(id);

            if (coingrade != null)
            {
                CoinGrade = coingrade;
                _context.CoinGrades.Remove(CoinGrade);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
