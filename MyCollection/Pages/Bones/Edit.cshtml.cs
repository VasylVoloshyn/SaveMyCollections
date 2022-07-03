using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly MyCollection.Data.MyCollectionContext _context;

        public EditModel(MyCollection.Data.MyCollectionContext context)
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

            var bone = await _context.Bones.Include(b => b.BonePhotos).ThenInclude(b => b.Photo).FirstOrDefaultAsync(m => m.Id == id);
            if (bone == null)
            {
                return NotFound();
            }
            Bone = bone;
            foreach (var photo in Bone.BonePhotos.Select(b => b.Photo))
            {
                photo.PreviewImageUrl = ImageService.GetImageUrl(photo.PreviewImageData);
            }

            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code");
            ViewData["GradeID"] = new SelectList(_context.Grades, "Id", "Code");
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

            Bone.BonePhotos = boneToUpdate.BonePhotos;
            List<Photo> photoToRemove = new List<Photo>();
            if (aversImage != null)
            {
                photoToRemove.Add(Bone.BonePhotos.First().Photo);
                Bone.BonePhotos.First().Photo = await ImageService.CreateImageAsync(aversImage);
            }

            if (reversImage != null)
            {
                photoToRemove.Add(Bone.BonePhotos.Last().Photo);
                Bone.BonePhotos.Last().Photo = await ImageService.CreateImageAsync(reversImage);
            }

            _context.Photos.RemoveRange(photoToRemove);
            _context.Attach(Bone).State = EntityState.Modified;

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
