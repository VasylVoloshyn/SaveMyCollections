using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(MyCollectionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

      public Bone Bone { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bones == null)
            {
                return NotFound();
            }

            var bone = await _context.Bones
                .Include(b => b.User)
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
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    if (bone.User?.Id == user.Id)
                    {
                        bone.AllowEdit = true;
                    }
                }
                Bone = bone;                
            }
            return Page();
        }
    }
}
