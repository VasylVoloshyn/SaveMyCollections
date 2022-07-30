using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Signatures
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
      public Signature Signature { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Signatures == null)
            {
                return NotFound();
            }

            var signature = await _context.Signatures
                .Include(s=>s.Person)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (signature == null)
            {
                return NotFound();
            }
            else 
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || signature.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                Signature = signature;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Signatures == null)
            {
                return NotFound();
            }
            var signature = await _context.Signatures.FindAsync(id);

            if (signature != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || signature.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                Signature = signature;
                _context.Signatures.Remove(Signature);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
