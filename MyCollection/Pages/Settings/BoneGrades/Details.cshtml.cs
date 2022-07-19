using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Grades
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

      public BoneGrade Grade { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BoneGrades == null)
            {
                return NotFound();
            }

            var grade = await _context.BoneGrades.FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }
            else 
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    if (grade.User?.Id == user.Id)
                    {
                        grade.AllowEdit = true;
                    }
                }
                Grade = grade;
            }
            return Page();
        }
    }
}
