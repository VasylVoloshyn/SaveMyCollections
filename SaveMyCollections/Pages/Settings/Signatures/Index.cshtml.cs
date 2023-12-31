﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;

namespace SaveMyCollections.Pages.Signatures
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SaveMyCollectionsContext _context;

        public IndexModel(SaveMyCollectionsContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Signature> Signature { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Signatures != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user?.Id;
                Signature = await _context.Signatures
                    .Where(s => s.User == null || s.User.Id == userId)
                    .Include(s => s.Person).ToListAsync();

                if (user != null)
                {
                    foreach (var signature in Signature)
                    {
                        if (signature.User?.Id == user.Id)
                        {
                            signature.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
