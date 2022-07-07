using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Stamps
{
    public class DetailsModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public DetailsModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

      public Stamp Stamp { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stamps == null)
            {
                return NotFound();
            }

            var stamp = await _context.Stamps.FirstOrDefaultAsync(m => m.Id == id);
            if (stamp == null)
            {
                return NotFound();
            }
            else 
            {
                Stamp = stamp;
            }
            return Page();
        }
    }
}
