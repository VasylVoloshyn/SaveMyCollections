﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.StampTypes
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

        public IList<StampType> StampType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.StampTypes != null)
            {
                var user = await _userManager.GetUserAsync(User);
                StampType = await _context.StampTypes
                    .Where(s => s.User == user || s.User == null)
                    .ToListAsync();

                if (user != null)
                {
                    foreach (var stampType in StampType)
                    {
                        if (stampType.User == user)
                        {
                            stampType.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
