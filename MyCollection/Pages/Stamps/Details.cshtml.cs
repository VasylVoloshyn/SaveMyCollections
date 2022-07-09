using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Stamps
{
    public class DetailsModel : PageModel
    {
        private readonly MyCollectionContext _context;

        public DetailsModel(MyCollectionContext context)
        {
            _context = context;
        }

        public Stamp Stamp { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stamps == null)
            {
                return NotFound();
            }

            var stamp = await _context.Stamps
                .Include(s => s.Country)
                .Include(s => s.Currency)
                .Include(s => s.Dime)
                .Include(s => s.StampGrade)
                .Include(s => s.StampPhoto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stamp == null)
            {
                return NotFound();
            }
            else
            {
                Stamp = stamp;
                if (Stamp.StampPhoto != null)
                {
                    Stamp.StampPhoto.ImageUrl = ImageService.GetImageUrl(Stamp.StampPhoto.ImageData);
                }
            }

            return Page();
        }
    }
}
