
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Bones
{
    public class DeleteModel : PageModel
    {
        private readonly MyCollectionContext _context;

        public DeleteModel(MyCollectionContext context)
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
                Bone = bone;
                foreach (var photo in Bone.BonePhotos.Select(b => b.Photo))
                {
                    photo.PreviewImageUrl = ImageService.GetImageUrl(photo.PreviewImageData);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Bones == null)
            {
                return NotFound();
            }
            var bone = await _context.Bones
                .Include(m => m.BonePhotos)
                .ThenInclude(m => m.Photo)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (bone != null)
            {
                Bone = bone;
                _context.Bones.Remove(Bone);
                var photos = Bone.BonePhotos.Select(o => o.Photo);
                foreach (var photo in photos)
                {
                    _context.Photos.Remove(photo);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
