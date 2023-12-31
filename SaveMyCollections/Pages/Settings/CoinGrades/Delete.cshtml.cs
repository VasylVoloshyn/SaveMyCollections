﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.CoinGrades
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
      public CoinGrade CoinGrade { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CoinGrades == null)
            {
                return NotFound();
            }

            var coingrade = await _context.CoinGrades.FirstOrDefaultAsync(m => m.Id == id);

            if (coingrade == null)
            {
                return NotFound();
            }
            else 
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || coingrade.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                CoinGrade = coingrade;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CoinGrades == null)
            {
                return NotFound();
            }
            var coingrade = await _context.CoinGrades.FindAsync(id);

            if (coingrade != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || coingrade.User?.Id != user.Id)
                {
                    return RedirectToPage("/General/AccessDenied");
                }
                CoinGrade = coingrade;
                _context.CoinGrades.Remove(CoinGrade);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
