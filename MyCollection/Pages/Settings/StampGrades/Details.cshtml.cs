using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.StampGrades
{
    public class DetailsModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(MyCollectionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    if (stampgrade.User == user)
                    {
                        stampgrade.AllowEdit = true;
                    }
                }
                StampGrade = stampgrade;
            }
            return Page();
        }
    }
}
