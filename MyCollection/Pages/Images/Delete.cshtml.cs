using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Images
{
    public class DeleteModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public DeleteModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Image Image { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images.FirstOrDefaultAsync(m => m.Id == id);

            if (image == null)
            {
                return NotFound();
            }
            else 
            {
                Image = image;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }
            var image = await _context.Images.FindAsync(id);

            if (image != null)
            {
                Image = image;
                _context.Images.Remove(Image);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
