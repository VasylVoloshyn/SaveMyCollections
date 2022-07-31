using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollectionsEnums;
using SaveMyCollections.Models;
using SaveMyCollections.Services;

namespace SaveMyCollections.Pages.Bones
{
    [Authorize(Roles = "Basic")]
    public class EditModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(SaveMyCollectionsContext context, IWebHostEnvironment hostingEnv,
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
                .Include( b=> b.User)
                .Include(b => b.BanknotePhoto)
                .ThenInclude(b => b.Photo).FirstOrDefaultAsync(m => m.Id == id);
            if (bone == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null || bone.User?.Id != user.Id)
            {
                return RedirectToPage("/General/AccessDenied");
            }
            Bone = bone;
            
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Code");
            ViewData["GradeID"] = new SelectList(_context.BanknoteGrades, "Id", "Code");
            ViewData["SignatureId"] = new SelectList(_context.Signatures, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, IFormFile? aversImage, IFormFile? reversImage)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Attach(Bone).State = EntityState.Modified;

            var boneToUpdate = await _context.Banknotes
                .Include(b => b.User)
                .Include(b => b.BanknotePhoto)                
                .ThenInclude(b => b.Photo).FirstOrDefaultAsync(b => b.Id == id);

            if (boneToUpdate == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            bool isAversExist = Bone.BanknotePhoto.Any(c => c.IsAvers);
            bool isReversExist = Bone.BanknotePhoto.Any(c => c.IsRevers);
            
            List<UserPhoto> photoToRemove = new List<UserPhoto>();
            if (aversImage != null)
            {
                if (isAversExist)
                {
                    photoToRemove.Add(Bone.BanknotePhoto.Where(c => c.IsAvers).First().Photo);
                    Bone.BanknotePhoto.Where(c => c.IsAvers).First().Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Banknote, aversImage, user);
                }
                else
                {
                    var avers = new BanknotePhoto();
                    avers.BanknoteId = Bone.Id;
                    avers.Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Banknote, aversImage, user);
                    avers.IsAvers = true;
                    Bone.BanknotePhoto.Add(avers);
                }
            }

            if (reversImage != null)
            {
                if (isReversExist)
                {
                    photoToRemove.Add(Bone.BanknotePhoto.Where(c => c.IsRevers).First().Photo);
                    Bone.BanknotePhoto.Where(c => c.IsRevers).First().Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Banknote, reversImage, user);
                }
                else
                {
                    var revers = new BanknotePhoto();
                    revers.BanknoteId = Bone.Id;
                    revers.Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Banknote, reversImage, user);
                    revers.IsRevers = true;
                    Bone.BanknotePhoto.Add(revers);
                }
            }

            foreach(var photo in photoToRemove)
            {
                await UserPhotoServise.DeletePhotoAsync(_hostingEnv, photo);
            }
            _context.UserPhotos.RemoveRange(photoToRemove);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoneExists(Bone.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BoneExists(int id)
        {
            return (_context.Banknotes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
