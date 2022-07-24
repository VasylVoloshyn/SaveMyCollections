using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;
using SaveMyCollections.Services;

namespace SaveMyCollections.Pages.Coins
{
    public class IndexModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly IConfiguration Configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(SaveMyCollectionsContext context, IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            Configuration = configuration;
            _userManager = userManager;
        }

        public string DimeSort { get; set; }
        public string YearSort { get; set; }
        public string NominalSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Coin> Coin { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            DimeSort = string.IsNullOrEmpty(sortOrder) ? "dime_desc" : "";
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
            ViewData["CurrencyId"] = new SelectList(_context.Dimes, "Id", "Code", CurrentFilter);

            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            IQueryable<Coin> coins = _context.Coins
                .Include(c => c.User)
                .Include(c => c.Dime)
                .Include(c => c.CoinGrade)
                .Include(c => c.CoinPhotos)
                .ThenInclude(c => c.Photo)
                .Where(c => c.User == null || c.User.Id == userId)
                .Select(c => c);

            if (!string.IsNullOrEmpty(searchString))
            {
                coins = coins                   
                    .Where(s => s.DimeId.ToString() == searchString);
            }
            switch (sortOrder)
            {
                case "dime_desc":
                    coins = coins.OrderByDescending(s => s.DimeId);
                    break;
                case "Nominal":
                    coins = coins.OrderBy(s => s.Nominal);
                    break;
                case "nominal_desc":
                    coins = coins.OrderByDescending(s => s.Nominal);
                    break;
                case "Year":
                    coins = coins.OrderBy(s => s.Year);
                    break;
                case "year_desc":
                    coins = coins.OrderByDescending(s => s.Year);
                    break;
                default:
                    coins = coins.OrderBy(s => s.DimeId);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Coin = await PaginatedList<Coin>.CreateAsync(coins.AsNoTracking(), pageIndex ?? 1, pageSize);

            if (user != null)
            {
                foreach (var coin in Coin)
                {
                    if (coin.User?.Id == user.Id)
                    {
                        coin.AllowEdit = true;
                    }
                }
            }
        }
    }
}
