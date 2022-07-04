using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.CoinGrades
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
            return Page();
        }

        [BindProperty]
        public CoinGrade CoinGrade { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.CoinGrades == null || CoinGrade == null)
            {
                return Page();
            }

            _context.CoinGrades.Add(CoinGrade);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
