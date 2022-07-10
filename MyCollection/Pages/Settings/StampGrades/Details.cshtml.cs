using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.StampGrades
{
    public class DetailsModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public DetailsModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

      public StampGrade StampGrade { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.StampGrades == null)
            {
                return NotFound();
            }

            var stampgrade = await _context.StampGrades.FirstOrDefaultAsync(m => m.Id == id);
            if (stampgrade == null)
            {
                return NotFound();
            }
            else 
            {
                StampGrade = stampgrade;
            }
            return Page();
        }
    }
}
