using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Stamps
{
    public class EditModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public EditModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Stamp Stamp { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stamps == null)
            {
                return NotFound();
            }

            var stamp =  await _context.Stamps.FirstOrDefaultAsync(m => m.Id == id);
            if (stamp == null)
            {
                return NotFound();
            }
            Stamp = stamp;
           ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Code");
           ViewData["DimeId"] = new SelectList(_context.Dimes, "Id", "Code");
           ViewData["StampGradeId"] = new SelectList(_context.StampGrades, "Id", "Code");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Stamp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StampExists(Stamp.Id))
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

        private bool StampExists(int id)
        {
          return (_context.Stamps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
