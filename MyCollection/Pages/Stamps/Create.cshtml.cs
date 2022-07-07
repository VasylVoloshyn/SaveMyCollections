using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Stamps
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
        ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Code");
        ViewData["DimeId"] = new SelectList(_context.Dimes, "Id", "Code");
        ViewData["StampGradeId"] = new SelectList(_context.StampGrades, "Id", "Code");
            return Page();
        }

        [BindProperty]
        public Stamp Stamp { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Stamps == null || Stamp == null)
            {
                return Page();
            }

            _context.Stamps.Add(Stamp);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
