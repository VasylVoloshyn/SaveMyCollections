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
    public class CreateModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;
        
        public CreateModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var signatures = _context.Signatures.Include(s => s.Person);
            foreach(var signatrure in signatures)
            {
                signatrure.PersonName =  signatrure.Person.Name + " " + signatrure.Person.FamilyName;
            }

            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code");
            ViewData["GradeID"] = new SelectList(_context.Grades, "Id", "Code");
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

            
            var bonePhotos = new List<BonePhoto>();
            if (aversImage != null)
            {
                var avers = new BonePhoto();
                avers.BoneId = Bone.Id;
                avers.Photo = await ImageService.CreateImageAsync(aversImage);
                bonePhotos.Add(avers);
            }

            if (reversImage != null)
            {
                var revers = new BonePhoto();
                revers.BoneId = Bone.Id;
                revers.Photo = await ImageService.CreateImageAsync(reversImage);
                bonePhotos.Add(revers);
            }
            
            Bone.BonePhotos = bonePhotos;

            _context.Bones.Add(Bone);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
