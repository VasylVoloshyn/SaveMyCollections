using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Grades
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

        public IList<BoneGrade> Grade { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BoneGrades != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user?.Id;
                Grade = await _context.BoneGrades
                    .Where(b => b.User == null || b.User.Id == userId)
                    .ToListAsync();

                if (user != null)
                {
                    foreach (var grade in Grade)
                    {
                        if (grade.User?.Id == user.Id)
                        {
                            grade.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
