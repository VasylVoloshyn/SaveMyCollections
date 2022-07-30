using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.StampTypes
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
                var user = await _userManager.GetUserAsync(User);
                if (user == null || stamptype.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
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
                var user = await _userManager.GetUserAsync(User);
                if (user == null || stamptype.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                StampType = stamptype;
                _context.StampTypes.Remove(StampType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
