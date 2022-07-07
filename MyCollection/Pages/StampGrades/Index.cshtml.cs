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
    public class IndexModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public IndexModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

        public IList<StampGrade> StampGrade { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.StampGrades != null)
            {
                StampGrade = await _context.StampGrades.ToListAsync();
            }
        }
    }
}
