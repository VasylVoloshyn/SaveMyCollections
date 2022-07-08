﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using MyCollection.Models;
using MyCollection.Service;

namespace MyCollection.Pages.Stamps
{
    public class IndexModel : PageModel
    {
        private readonly MyCollection.Data.MyCollectionContext _context;

        public IndexModel(MyCollection.Data.MyCollectionContext context)
        {
            _context = context;
        }

        public IList<Stamp> Stamp { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Stamps != null)
            {
                Stamp = await _context.Stamps
                .Include(s => s.Currency)
                .Include(s => s.Dime)
                .Include(s => s.StampGrade)
                .Include(s=>s.StampPhoto)
                .ToListAsync();
                foreach (var stamp in Stamp)
                {
                    stamp.StampPhoto.PreviewImageUrl = ImageService.GetImageUrl(stamp.StampPhoto.PreviewImageData);
                }
            }
        }
    }
}
