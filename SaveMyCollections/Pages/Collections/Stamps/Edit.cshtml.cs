﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollectionsEnums;
using SaveMyCollections.Models;
using SaveMyCollections.Services;

namespace SaveMyCollections.Pages.Stamps
{
    [Authorize(Roles = "Basic")]
    public class EditModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(SaveMyCollectionsContext context, IWebHostEnvironment hostingEnv,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }

        [BindProperty]
        public Stamp Stamp { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stamps == null)
            {
                return NotFound();
            }

            var stamp = await _context.Stamps
                .Include(s => s.User)
                .Include(s => s.Country)
                .Include(s => s.StampGrade)
                .Include(s => s.Currency)
                .Include(c => c.Dime)
                .Include(c => c.StampPhoto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stamp == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null || stamp.User?.Id != user.Id)
            {
                return RedirectToPage("/General/AccessDenied");
            }

            Stamp = stamp;
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Name");
            ViewData["DimeId"] = new SelectList(_context.Dimes, "Id", "Name");
            ViewData["StampGradeId"] = new SelectList(_context.StampGrades, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, IFormFile? stampImage)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var stampToUpdate = await _context.Stamps
                .AsNoTracking()
                .Include(b => b.StampPhoto)
                .FirstOrDefaultAsync(b => b.Id == id);

            _context.Entry(Stamp).State = EntityState.Modified;
            if (stampToUpdate == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            Stamp.StampPhoto = stampToUpdate.StampPhoto;
            bool isPhotoExist = Stamp.StampPhoto != null;
            UserPhoto? photoToRemove = null;

            if (stampImage != null)
            {
                if (isPhotoExist)
                {
                    photoToRemove = Stamp.StampPhoto;
                    Stamp.StampPhoto = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Stamp, stampImage, user);
                }
                else
                {
                    Stamp.StampPhoto = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Stamp, stampImage, user);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StampExists(Stamp.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            if (photoToRemove != null)
            {
                await UserPhotoServise.DeletePhotoAsync(_hostingEnv, photoToRemove);
                _context.UserPhotos.Remove(photoToRemove);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StampExists(Stamp.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StampExists(int id)
        {
            return (_context.Stamps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
