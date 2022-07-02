using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Models;

namespace MyCollection.Pages.Countries
{
    public class IndexModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(MyCollection.Data.MyCollectionContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string CodeSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Country> Country { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CodeSort = sortOrder == "Code" ? "code_desc" : "Code";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Country> countries = _context.Countries.Select(c => c);
                                             
            if (!String.IsNullOrEmpty(searchString))
            {
                countries = countries.Where(s => s.Name.Contains(searchString))                                      ;
            }
            switch (sortOrder)
            {
                case "name_desc":
                    countries = countries.OrderByDescending(s => s.Name);
                    break;
                case "Code":
                    countries = countries.OrderBy(s => s.Code);
                    break;
                case "code_desc":
                    countries = countries.OrderByDescending(s => s.Code);
                    break;
                default:
                    countries = countries.OrderBy(s => s.Name);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Country = await PaginatedList<Country>.CreateAsync(
                countries .AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
