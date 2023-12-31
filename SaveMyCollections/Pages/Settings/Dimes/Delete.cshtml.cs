﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Settings.Dimes
{
    [Authorize(Roles = "Basic")]
    public class DeleteModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(SaveMyCollectionsContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
      public Dime Dime { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Dimes == null)
            {
                return NotFound();
            }

            var dime = await _context.Dimes
                .Include(d => d.Country)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dime == null)
            {
                return NotFound();
            }
            else 
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || dime.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                Dime = dime;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Dimes == null)
            {
                return NotFound();
            }
            var dime = await _context.Dimes.FindAsync(id);

            if (dime != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || dime.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                Dime = dime;
                _context.Dimes.Remove(Dime);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
