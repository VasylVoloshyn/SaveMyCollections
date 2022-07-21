using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.StampTypes
{
    public class IndexModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(SaveMyCollectionsContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<StampType> StampType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.StampTypes != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user?.Id;
                StampType = await _context.StampTypes
                    .Where(s => s.User == null || s.User.Id == userId)
                    .ToListAsync();

                if (user != null)
                {
                    foreach (var stampType in StampType)
                    {
                        if (stampType.User?.Id == user.Id)
                        {
                            stampType.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
