using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Persons
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

        public IList<Person> Person { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);            
            if (_context.Persons != null)
            {
                Person = await _context.Persons
                    .Where(p => p.User == null || p.User.Id == user.Id)
                    .ToListAsync();

                if (user != null)
                {
                    foreach (var person in Person)
                    {
                        if (person.User?.Id == user.Id)
                        {
                            person.AllowEdit = true;
                        }
                    }
                }
            }
        }
    }
}
