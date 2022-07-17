﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Grades
{
    public class IndexModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(MyCollectionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<BoneGrade> Grade { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BoneGrades != null)
            {
                var user = await _userManager.GetUserAsync(User);
                Grade = await _context.BoneGrades
                    .Where(b => b.User == user || b.User == null)
                    .ToListAsync();

                if (user != null)
                {
                    foreach (var grade in Grade)
                    {
                        if (grade.User == user)
                        {
                            grade.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}