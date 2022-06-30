using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Signatures
{
    public class DetailsModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public DetailsModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

      public Signature Signature { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Signatures == null)
            {
                return NotFound();
            }

            var signature = await _context.Signatures.FirstOrDefaultAsync(m => m.Id == id);
            if (signature == null)
            {
                return NotFound();
            }
            else 
            {
                Signature = signature;
            }
            return Page();
        }
    }
}
