using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crisan_Daniel_Lab2.Data;
using Crisan_Daniel_Lab2.Models;
using Crisan_Daniel_Lab2.Models.ViewModels;

namespace Crisan_Daniel_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Crisan_Daniel_Lab2.Data.Crisan_Daniel_Lab2Context _context;

        public IndexModel(Crisan_Daniel_Lab2.Data.Crisan_Daniel_Lab2Context context)
        {
            _context = context;
        }

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            CategoryData = new CategoryIndexData();

            CategoryData.Categories = await _context.Category
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Author)
                .OrderBy(i => i.CategoryName)
                .AsNoTracking()
                .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;
                Category category = CategoryData.Categories
                    .Where(i => i.ID == id.Value).Single();
                CategoryData.Books = category.BookCategories.Select(bc => bc.Book);
            }
        }
    }
}
