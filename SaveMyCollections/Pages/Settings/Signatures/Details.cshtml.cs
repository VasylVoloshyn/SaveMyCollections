using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Signatures
{
    public class DetailsModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(SaveMyCollectionsContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

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
                if (user != null)
                {
                    if (signature.User?.Id == user.Id)
                    {
                        signature.AllowEdit = true;
                    }
                }
                Signature = signature;
            }
            return Page();
        }
    }
}
