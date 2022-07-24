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
    public class CreateModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(SaveMyCollectionsContext context, IWebHostEnvironment hostingEnv,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            var signatures = _context.Signatures.Include(s => s.Person);
            foreach(var signatrure in signatures)
            {
                signatrure.PersonName =  signatrure.Person.Name + " " + signatrure.Person.FamilyName;
            }

            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Code");
            ViewData["GradeID"] = new SelectList(_context.BoneGrades, "Id", "Code");
            ViewData["SignatureId"] = new SelectList(signatures, "Id", "PersonName");
             
            return Page();
        }

        [BindProperty]
        public Bone Bone { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile? aversImage , IFormFile? reversImage)
        {
          if (!ModelState.IsValid || _context.Bones == null || Bone == null)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
                                    
            var bonePhotos = new List<BonePhoto>();
            if (aversImage != null)
            {
                var avers = new BonePhoto();
                avers.BoneId = Bone.Id;
                avers.Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Bone, aversImage, user);                
                avers.IsAvers = true;
                bonePhotos.Add(avers);
            }

            if (reversImage != null)
            {
                var revers = new BonePhoto();
                revers.BoneId = Bone.Id;                
                revers.Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Bone, reversImage, user);
                revers.IsRevers = true;
                bonePhotos.Add(revers);
            }
            
            Bone.BonePhotos = bonePhotos;
            Bone.User = user;

            _context.Bones.Add(Bone);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
