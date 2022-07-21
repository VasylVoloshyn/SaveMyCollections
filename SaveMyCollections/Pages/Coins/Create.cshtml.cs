using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SaveMyCollections.Data;
using SaveMyCollectionsEnums;
using SaveMyCollections.Models;
using SaveMyCollectionsService;

namespace SaveMyCollections.Pages.Coins
{
    [Authorize(Roles = "Basic")]
    public class CreateModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(SaveMyCollectionsContext context, IWebHostEnvironment hostingEnv,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["CoinGradeId"] = new SelectList(_context.CoinGrades, "Id", "Code");
        ViewData["DimeId"] = new SelectList(_context.Dimes, "Id", "Code");
            return Page();
        }

        [BindProperty]
        public Coin Coin { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile? aversImage, IFormFile? reversImage)
        {
          if (!ModelState.IsValid || _context.Coins == null || Coin == null)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            var coinPhotos = new List<CoinPhoto>();
            if (aversImage != null)
            {
                var avers = new CoinPhoto();
                avers.CoinId = Coin.Id;
                avers.Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Coin, aversImage, user);                
                avers.IsAvers = true;
                coinPhotos.Add(avers);
            }

            if (reversImage != null)
            {
                var revers = new CoinPhoto();
                revers.CoinId = Coin.Id;
                revers.Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Coin, reversImage, user);                
                revers.IsRevers = true;
                coinPhotos.Add(revers);
            }

            Coin.CoinPhotos = coinPhotos;
            Coin.User = user;

            _context.Coins.Add(Coin);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
