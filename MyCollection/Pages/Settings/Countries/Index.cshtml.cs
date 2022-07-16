using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Countries
{
    public class IndexModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(MyCollectionContext context,
            UserManager<ApplicationUser> userManager)            
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Country> Country { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Countries != null)
            {
                var user = await _userManager.GetUserAsync(User);
                Country = await _context.Countries
                    .Where(c => c.User == user || c.User == null)
                    .ToListAsync();

                if (user != null)
                {
                    foreach (var country in Country)
                    {
                        if (country.User == user)
                        {
                            country.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
