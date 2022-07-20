using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.StampGrades
{
    public class IndexModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(MyCollectionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<StampGrade> StampGrade { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.StampGrades != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user?.Id;
                StampGrade = await _context.StampGrades
                    .Where(s => s.User == null || s.User.Id == userId)
                    .ToListAsync();

                if (user != null)
                {
                    foreach (var stampGrade in StampGrade)
                    {
                        if (stampGrade.User?.Id == user.Id)
                        {
                            stampGrade.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
