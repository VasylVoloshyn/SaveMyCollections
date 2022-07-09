﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Stamps
{
    public class CreateModel : PageModel
    {
        private readonly MyCollectionContext _context;

        public CreateModel(MyCollectionContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Code");
            ViewData["DimeId"] = new SelectList(_context.Dimes, "Id", "Code");
            ViewData["StampGradeId"] = new SelectList(_context.StampGrades, "Id", "Code");
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

            if (stampImage != null)
            {                
                Photo stampPhoto = await ImageService.CreateImageAsync(stampImage);
                Stamp.StampPhoto = stampPhoto;
            }

            _context.Stamps.Add(Stamp);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
