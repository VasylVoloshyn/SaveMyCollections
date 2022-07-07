using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Bones
{
    public class EditModel : PageModel
    {
        private readonly MyCollectionContext _context;

        public EditModel(MyCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bone Bone { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bones == null)
            {
                return NotFound();
            }

            var bone = await _context.Bones
                .Include(b => b.BonePhotos)
                .ThenInclude(b => b.Photo).FirstOrDefaultAsync(m => m.Id == id);
            if (bone == null)
            {
                return NotFound();
            }
            Bone = bone;
            foreach (var photo in Bone.BonePhotos.Select(b => b.Photo))
            {
                photo.PreviewImageUrl = ImageService.GetImageUrl(photo.PreviewImageData);
            }

            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Code");
            ViewData["GradeID"] = new SelectList(_context.BoneGrades, "Id", "Code");
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

            var boneToUpdate = await _context.Bones.Include(b => b.BonePhotos)
                .ThenInclude(b => b.Photo).FirstOrDefaultAsync(b => b.Id == id);

            if (boneToUpdate == null)
            {
                return NotFound();
            }

            bool isAversExist = Bone.BonePhotos.Any(c => c.IsAvers);
            bool isReversExist = Bone.BonePhotos.Any(c => c.IsRevers);
            
            List<Photo> photoToRemove = new List<Photo>();
            if (aversImage != null)
            {
                if (isAversExist)
                {
                    photoToRemove.Add(Bone.BonePhotos.Where(c => c.IsAvers).First().Photo);
                    Bone.BonePhotos.Where(c => c.IsAvers).First().Photo = await ImageService.CreateImageAsync(aversImage);
                }
                else
                {
                    var avers = new BonePhoto();
                    avers.BoneId = Bone.Id;
                    avers.Photo = await ImageService.CreateImageAsync(aversImage);
                    avers.IsAvers = true;
                    Bone.BonePhotos.Add(avers);
                }
            }

            if (reversImage != null)
            {
                if (isReversExist)
                {
                    photoToRemove.Add(Bone.BonePhotos.Where(c => c.IsRevers).First().Photo);
                    Bone.BonePhotos.Where(c => c.IsRevers).First().Photo = await ImageService.CreateImageAsync(reversImage);
                }
                else
                {
                    var revers = new BonePhoto();
                    revers.BoneId = Bone.Id;
                    revers.Photo = await ImageService.CreateImageAsync(reversImage);
                    revers.IsRevers = true;
                    Bone.BonePhotos.Add(revers);
                }
            }

            _context.Photos.RemoveRange(photoToRemove);

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
            return (_context.Bones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
