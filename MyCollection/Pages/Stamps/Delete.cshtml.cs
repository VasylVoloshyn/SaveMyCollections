using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Stamps
{
    public class DeleteModel : PageModel
    {
        private readonly MyCollectionContext _context;

        public DeleteModel(MyCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Stamp Stamp { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stamps == null)
            {
                return NotFound();
            }            

            var stamp = await _context.Stamps
                .Include(s => s.StampGrade)
                .Include(s => s.Currency)
                .Include(c => c.Dime)
                .Include(c => c.StampPhoto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (stamp == null)
            {
                return NotFound();
            }
            else
            {
                Stamp = stamp;
                
               Stamp.StampPhoto.PreviewImageUrl = ImageService.GetImageUrl(Stamp.StampPhoto.PreviewImageData);                
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Stamps == null)
            {
                return NotFound();
            }
            
            var stamp = await _context.Stamps
                .Include(m => m.StampPhoto)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (stamp != null)
            {
                Stamp = stamp;
                _context.Stamps.Remove(Stamp);
                var photo = Stamp.StampPhoto;                                
                _context.Photos.Remove(photo);
                
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
