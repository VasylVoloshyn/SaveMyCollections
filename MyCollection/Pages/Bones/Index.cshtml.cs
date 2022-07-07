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

namespace MyCollection.Pages.Bones
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

        public string CurrencySort { get; set; }
        public string YearSort { get; set; }
        public string NominalSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Bone> Bone { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            CurrencySort = String.IsNullOrEmpty(sortOrder) ? "currency_desc" : "";
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
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Code", CurrentFilter);

            IQueryable<Bone> bones = _context.Bones
                .Include(c=>c.Currency)
                .Include(c=>c.Signature)
                .ThenInclude(c=>c.Person)
                .Include(c=>c.Grade)
                .Include(c=>c.BonePhotos)
                .ThenInclude(b=>b.Photo)
                .Select(b => b);


            if (!String.IsNullOrEmpty(searchString))
            {
                bones = bones.Where(s => s.CurrencyId.ToString() == searchString);
            }
            switch (sortOrder)
            {
                case "currency_desc":
                    bones = bones.OrderByDescending(s => s.CurrencyId);
                    break;
                case "Nominal":
                    bones= bones.OrderBy(s => s.Nominal);
                    break;
                case "nominal_desc":
                    bones = bones.OrderByDescending(s => s.Nominal);
                    break;
                case "Year":
                    bones = bones.OrderBy(s => s.Year);
                    break;
                case "year_desc":
                    bones = bones.OrderByDescending(s => s.Year);
                    break;
                default:
                    bones = bones.OrderBy(s => s.CurrencyId);
                    break;
            }
           
            var pageSize = Configuration.GetValue("PageSize", 4);
            Bone = await PaginatedList<Bone>.CreateAsync(bones.AsNoTracking(), pageIndex ?? 1, pageSize);

            foreach (var img in Bone.Select(b => b.BonePhotos))
            {
                foreach (var photo in img)
                {
                    photo.Photo.PreviewImageUrl = ImageService.GetImageUrl(photo.Photo.PreviewImageData);
                }
            }
        }
    }    
}
