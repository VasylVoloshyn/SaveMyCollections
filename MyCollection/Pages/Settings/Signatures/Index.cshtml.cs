using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Signatures
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyCollectionContext _context;

        public IndexModel(MyCollectionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Signature> Signature { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Signatures != null)
            {
                var user = await _userManager.GetUserAsync(User);
                Signature = await _context.Signatures
                    .Where(s => s.User == null || s.User.Id == user.Id)
                    .Include(s => s.Person).ToListAsync();

                if (user != null)
                {
                    foreach (var signature in Signature)
                    {
                        if (signature.User?.Id == user.Id)
                        {
                            signature.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
