using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.StampGrades
{
    [Authorize(Roles = "Basic")]
    public class DeleteModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(MyCollectionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
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
                if (user == null || stampgrade.User != user)
                {
                    return RedirectToPage("/AccessDenied");
                }
                StampGrade = stampgrade;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.StampGrades == null)
            {
                return NotFound();
            }
            var stampgrade = await _context.StampGrades.FindAsync(id);

            if (stampgrade != null)
            {
                StampGrade = stampgrade;
                _context.StampGrades.Remove(StampGrade);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
