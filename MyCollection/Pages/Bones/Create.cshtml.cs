using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Bones
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
        ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id");
        ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Id");
        ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id");
        ViewData["SignatureId"] = new SelectList(_context.Signatures, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Bone Bone { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Bones == null || Bone == null)
            {
                return Page();
            }

            _context.Bones.Add(Bone);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
