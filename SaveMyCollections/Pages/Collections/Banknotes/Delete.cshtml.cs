using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;
using SaveMyCollections.Services;

namespace SaveMyCollections.Pages.Bones
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
        public Banknote Bone { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Banknotes == null)
            {
                return NotFound();
            }

            var bone = await _context.Banknotes
                .Include(b=> b.User)
                .Include(b => b.BanknotePhoto)
                .ThenInclude(b => b.Photo)
                .Include(b => b.Signature)
                .ThenInclude(b => b.Person)
                .Include(b => b.Grade)
                .Include(b => b.Currency)
                .ThenInclude(b => b.Country)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bone == null)
            {
                return NotFound();
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || bone.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                Bone = bone;
                
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Banknotes == null)
            {
                return NotFound();
            }
            var bone = await _context.Banknotes
                .Include(b=> b.User)
                .Include(b => b.BanknotePhoto)
                .ThenInclude(b => b.Photo)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (bone != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || bone.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                Bone = bone;
                _context.Banknotes.Remove(Bone);
                var photos = Bone.BanknotePhoto.Select(o => o.Photo);
                foreach (var photo in photos)
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
