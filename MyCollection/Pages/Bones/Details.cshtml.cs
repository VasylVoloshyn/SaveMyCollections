using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Bones
{
    public class DetailsModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public DetailsModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

      public Bone Bone { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bones == null)
            {
                return NotFound();
            }

            var bone = await _context.Bones.FirstOrDefaultAsync(m => m.Id == id);
            if (bone == null)
            {
                return NotFound();
            }
            else 
            {
                Bone = bone;
            }
            return Page();
        }
    }
}
