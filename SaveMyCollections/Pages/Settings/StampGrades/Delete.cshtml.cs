using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.StampGrades
{
    [Authorize(Roles = "Basic")]
    public class DeleteModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(SaveMyCollectionsContext context, UserManager<ApplicationUser> userManager)
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
                if (user == null || stampgrade.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
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
                var user = await _userManager.GetUserAsync(User);
                if (user == null || stampgrade.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                StampGrade = stampgrade;
                _context.StampGrades.Remove(StampGrade);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
