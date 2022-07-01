using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Signatures
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
        ViewData["PersonId"] = new SelectList(_context.Person, "Id", "FamilyName");
            return Page();
        }

        [BindProperty]
        public Signature Signature { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Signatures == null || Signature == null)
            {
                return Page();
            }

            _context.Signatures.Add(Signature);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
