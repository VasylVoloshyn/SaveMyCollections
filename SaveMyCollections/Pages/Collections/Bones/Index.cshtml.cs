using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollections.Models;
using SaveMyCollections.Services;

namespace SaveMyCollections.Pages.Bones
{
    public class IndexModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly IConfiguration Configuration;       
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(SaveMyCollectionsContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            Configuration = configuration;
            _userManager = userManager;
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
            CurrencySort = string.IsNullOrEmpty(sortOrder) ? "currency_desc" : "";
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

            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            IQueryable<Bone> bones = _context.Bones                
                .Include(b => b.User)
                .Include(b=>b.Currency)
                .Include(b=>b.Signature)
                .ThenInclude(b=>b.Person)
                .Include(b=>b.Grade)
                .Include(b=>b.BonePhotos)
                .ThenInclude(b=>b.Photo)
                .Where(b => b.User == null || b.User.Id == userId)
                .Select(b => b);


            if (!string.IsNullOrEmpty(searchString))
            {
                bones = bones                    
                    .Where(s => s.CurrencyId.ToString() == searchString);
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
            Bone = await PaginatedList<Bone>.CreateAsync(bones, pageIndex ?? 1, pageSize);

            if (user != null)
            {
                foreach (var bone in Bone)
                {
                    if (bone.User?.Id == user.Id)
                    {
                        bone.AllowEdit = true;
                    }
                }
            }
        }
    }    
}
