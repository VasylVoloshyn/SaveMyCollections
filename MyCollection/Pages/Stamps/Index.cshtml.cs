using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;

namespace MyCollection.Pages.Stamps
{
    public class IndexModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly IConfiguration Configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(MyCollectionContext context, IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            Configuration = configuration;
            _userManager = userManager;
        }

        public string CountrySort { get; set; }
        public string YearSort { get; set; }
        public string NominalSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Stamp> Stamp { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            CountrySort = string.IsNullOrEmpty(sortOrder) ? "country_desc" : "";
            NominalSort = sortOrder == "Nominal" ? "nominal_desc" : "Nominal";
            YearSort = sortOrder == "Year" ? "year_desc" : "Year";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", CurrentFilter);
            var user = await _userManager.GetUserAsync(User);
            IQueryable<Stamp> stamps = _context.Stamps
                .Where(s => s.User == null || s.User.Id == user.Id )
                .Include(s => s.User)
                .Include(s => s.Country)
                .Include(s => s.Currency)
                .Include(s => s.Dime)
                .Include(s => s.StampGrade)
                .Include(s => s.StampPhoto)                
                .Select(s => s);

            if (!string.IsNullOrEmpty(searchString))
            {
                stamps = stamps.Where(s => s.CountryId.ToString() == searchString);
            }

            switch (sortOrder)
            {
                case "country_desc":
                    stamps = stamps.OrderByDescending(s => s.CountryId);
                    break;
                case "Nominal":
                    stamps = stamps.OrderBy(s => s.Nominal);
                    break;
                case "nominal_desc":
                    stamps = stamps.OrderByDescending(s => s.Nominal);
                    break;
                case "Year":
                    stamps = stamps.OrderBy(s => s.Year);
                    break;
                case "year_desc":
                    stamps = stamps.OrderByDescending(s => s.Year);
                    break;
                default:
                    stamps = stamps.OrderBy(s => s.CountryId);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Stamp = await PaginatedList<Stamp>.CreateAsync(stamps.AsNoTracking(), pageIndex ?? 1, pageSize);
            if (user != null)
            {
                foreach (var stamp in Stamp)
                {
                    if (stamp.User?.Id == user.Id)
                    {
                        stamp.AllowEdit = true;
                    }
                }
            }
        }
    }
}
