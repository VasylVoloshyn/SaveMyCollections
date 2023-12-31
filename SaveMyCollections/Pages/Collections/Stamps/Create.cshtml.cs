﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SaveMyCollections.Data;
using SaveMyCollectionsEnums;
using SaveMyCollections.Models;
using SaveMyCollections.Services;

namespace SaveMyCollections.Pages.Stamps
{
    [Authorize(Roles = "Basic")]
    public class CreateModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(SaveMyCollectionsContext context, IWebHostEnvironment hostingEnv, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Name");
            ViewData["DimeId"] = new SelectList(_context.Dimes, "Id", "Name");
            ViewData["StampGradeId"] = new SelectList(_context.StampGrades, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Stamp Stamp { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile? stampImage)
        {
            if (!ModelState.IsValid || _context.Stamps == null || Stamp == null)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (stampImage != null)
            {
                var uPhoto = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Stamp, stampImage, user);                
                Stamp.StampPhoto = uPhoto;
            }

            Stamp.User = user;
            _context.Stamps.Add(Stamp);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
