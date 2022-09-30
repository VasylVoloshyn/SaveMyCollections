using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Settings.Materials
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

        public IList<Material> Materials { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Materials != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user?.Id;
                Materials = await _context.Materials
                    .Where(s => s.User == null || s.User.Id == userId)
                    .ToListAsync();

                if (user != null)
                {
                    foreach (var material in Materials)
                    {
                        if (material.User?.Id == user.Id)
                        {
                            material.AllowEdit = true;
                        }
                    }
                }
            }
        }

    }
}

