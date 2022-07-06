using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Bones
{
    public class DetailsModel : PageModel
    {
        private readonly MyCollectionContext _context;

        public DetailsModel(MyCollectionContext context)
        {
            _context = context;
        }

      public Bone Bone { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bones == null)
            {
                return NotFound();
            }

            var bone = await _context.Bones
                .Include(b=>b.BonePhotos)
                .ThenInclude(b=>b.Photo)
                .Include(b=>b.Signature)
                .ThenInclude(b=>b.Person)
                .Include(b=>b.Grade)
                .Include(b=>b.Currency)
                .ThenInclude(b=>b.Country)               
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
                    photo.ImageUrl = ImageService.GetImageUrl(photo.ImageData);
                }
            }
            return Page();
        }
    }
}
