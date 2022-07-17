﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.StampTypes
{
    [Authorize(Roles = "Basic")]
    public class CreateModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(MyCollectionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StampType StampType { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.StampTypes == null || StampType == null)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            StampType.User = user;

            _context.StampTypes.Add(StampType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
