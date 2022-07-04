using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Coins
{
    public class IndexModel : PageModel
    {
        private readonly MyCollectionContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(MyCollectionContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
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

            IQueryable<Coin> coins = _context.Coins
                .Include(c => c.Dime)
                .Include(c => c.CoinGrade)
                .Include(c => c.CoinPhotos)
                .ThenInclude(b => b.Photo)
                .Select(b => b);

            if (!string.IsNullOrEmpty(searchString))
            {
                coins = coins.Where(s => s.DimeId.ToString() == searchString);
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

            foreach (var img in Coin.Select(b => b.CoinPhotos))
            {
                foreach (var photo in img)
                {
                    photo.Photo.PreviewImageUrl = ImageService.GetImageUrl(photo.Photo.PreviewImageData);
                }
            }
        }
    }
}
