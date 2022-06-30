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

namespace MyCollection.Pages.Bones
{
    public class EditModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public EditModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bone Bone { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bones == null)
            {
                return NotFound();
            }

            var bone =  await _context.Bones.FirstOrDefaultAsync(m => m.Id == id);
            if (bone == null)
            {
                return NotFound();
            }
            Bone = bone;
           ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id");
           ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Id");
           ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id");
           ViewData["SignatureId"] = new SelectList(_context.Signatures, "Id", "Id");
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

            _context.Attach(Bone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoneExists(Bone.Id))
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

        private bool BoneExists(int id)
        {
          return (_context.Bones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
