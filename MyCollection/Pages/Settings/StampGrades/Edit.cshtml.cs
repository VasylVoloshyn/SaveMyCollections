﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.StampGrades
{
    public class EditModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public EditModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StampGrade StampGrade { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.StampGrades == null)
            {
                return NotFound();
            }

            var stampgrade =  await _context.StampGrades.FirstOrDefaultAsync(m => m.Id == id);
            if (stampgrade == null)
            {
                return NotFound();
            }
            StampGrade = stampgrade;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(StampGrade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StampGradeExists(StampGrade.Id))
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

        private bool StampGradeExists(int id)
        {
          return (_context.StampGrades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}