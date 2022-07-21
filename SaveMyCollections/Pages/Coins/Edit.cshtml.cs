using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaveMyCollections.Data;
using SaveMyCollectionsEnums;
using SaveMyCollections.Models;
using SaveMyCollectionsService;

namespace SaveMyCollections.Pages.Coins
{
    [Authorize(Roles = "Basic")]
    public class EditModel : PageModel
    {
        private readonly SaveMyCollectionsContext _context;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(SaveMyCollectionsContext context, IWebHostEnvironment hostingEnv,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
        }

        [BindProperty]
        public Coin Coin { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Coins == null)
            {
                return NotFound();
            }
            var coin = await _context.Coins
                .Include(c => c.User)
                .Include(c => c.CoinPhotos)
                .ThenInclude(c => c.Photo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coin == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null || coin.User?.Id != user.Id)
            {
                return RedirectToPage("/AccessDenied");
            }
            Coin = coin;
            

            ViewData["CoinGradeId"] = new SelectList(_context.CoinGrades, "Id", "Code");
            ViewData["DimeId"] = new SelectList(_context.Dimes, "Id", "Code");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, IFormFile? aversImage, IFormFile? reversImage)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Coin).State = EntityState.Modified;

            var coinToUpdate = await _context.Coins
                .Include(c => c.User)
                .Include(c => c.CoinPhotos)
                .ThenInclude(c => c.Photo)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (coinToUpdate == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            Coin.CoinPhotos = coinToUpdate.CoinPhotos;
            bool isAversExist = Coin.CoinPhotos.Any(c=>c.IsAvers);
            bool isReversExist = Coin.CoinPhotos.Any(c => c.IsRevers);
            
            List<UserPhoto> photoToRemove = new List<UserPhoto>();
            if (aversImage != null)
            {
                if (isAversExist)
                {
                    photoToRemove.Add(Coin.CoinPhotos.Where(c=>c.IsAvers).First().Photo);
                    Coin.CoinPhotos.Where(c => c.IsAvers).First().Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Coin, aversImage, user);
                }
                else
                {
                    var avers = new CoinPhoto();
                    avers.CoinId = Coin.Id;
                    Coin.CoinPhotos.Where(c => c.IsAvers).First().Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Coin, aversImage, user);
                    avers.IsAvers = true;
                    Coin.CoinPhotos.Add(avers);
                }
            }

            if (reversImage != null)
            {
                if (isReversExist)
                {
                    photoToRemove.Add(Coin.CoinPhotos.Where(c=>c.IsRevers).First().Photo);
                    Coin.CoinPhotos.Where(c=>c.IsRevers).First().Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Coin, reversImage, user);
                }
                else
                {
                    var revers = new CoinPhoto();
                    revers.CoinId = Coin.Id;
                    Coin.CoinPhotos.Where(c => c.IsRevers).First().Photo = await UserPhotoServise.CreateImageAsync(_hostingEnv, MyColectionType.Coin, reversImage, user);
                    revers.IsRevers = true;
                    Coin.CoinPhotos.Add(revers);
                }
            }

            foreach (var photo in photoToRemove)
            {
                await UserPhotoServise.DeletePhotoAsync(_hostingEnv, photo);
            }
            _context.UserPhotos.RemoveRange(photoToRemove);            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoinExists(Coin.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CoinExists(int id)
        {
          return (_context.Coins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
