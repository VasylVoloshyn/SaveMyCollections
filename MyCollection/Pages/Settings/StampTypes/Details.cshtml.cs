using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.StampTypes
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
                if (user != null)
                {
                    if (stamptype.User?.Id == user.Id)
                    {
                        stamptype.AllowEdit = true;
                    }
                }
                StampType = stamptype;
            }
            return Page();
        }
    }
}
