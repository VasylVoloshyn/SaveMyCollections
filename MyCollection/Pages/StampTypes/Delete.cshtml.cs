using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.StampTypes
{
    public class DeleteModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public DeleteModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
      public StampType StampType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.StampTypes == null)
            {
                return NotFound();
            }

            var stamptype = await _context.StampTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (stamptype == null)
            {
                return NotFound();
            }
            else 
            {
                StampType = stamptype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.StampTypes == null)
            {
                return NotFound();
            }
            var stamptype = await _context.StampTypes.FindAsync(id);

            if (stamptype != null)
            {
                StampType = stamptype;
                _context.StampTypes.Remove(StampType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
