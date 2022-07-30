using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;
using SaveMyCollections.Services;

namespace SaveMyCollections.Pages.Stamps
{
    [Authorize(Roles = "Basic")]
    public class DeleteModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(SaveMyCollectionsContext context, IWebHostEnvironment hostingEnv,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }

        [BindProperty]
        public Stamp Stamp { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stamps == null)
            {
                return NotFound();
            }

            var stamp = await _context.Stamps
                .Include(s => s.User)
                .Include(s => s.Country)
                .Include(s => s.StampGrade)
                .Include(s => s.Currency)
                .Include(s => s.Dime)
                .Include(s => s.StampPhoto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (stamp == null)
            {
                return NotFound();
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || stamp.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                Stamp = stamp;                
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Stamps == null)
            {
                return NotFound();
            }

            var stamp = await _context.Stamps
                .Include(m => m.StampPhoto)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (stamp != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || stamp.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                Stamp = stamp;
                _context.Stamps.Remove(Stamp);
                var photo = Stamp.StampPhoto;
                if (photo != null)
                {
                    await UserPhotoServise.DeletePhotoAsync(_hostingEnv, photo);
                    _context.UserPhotos.Remove(photo);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
