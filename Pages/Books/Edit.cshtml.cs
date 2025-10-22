using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crisan_Daniel_Lab2.Data;
using Crisan_Daniel_Lab2.Models;

namespace Crisan_Daniel_Lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Crisan_Daniel_Lab2.Data.Crisan_Daniel_Lab2Context _context;

        public EditModel(Crisan_Daniel_Lab2.Data.Crisan_Daniel_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            
            PopulateAssignedCategoryData(_context, Book);

            
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName", Book.PublisherID);
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName", Book.AuthorID);
            return Page();
        }

       
        
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                i => i.Title, i => i.Price, i => i.PublishingDate, i => i.PublisherID, i => i.AuthorID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            

            
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssignedCategoryData(_context, bookToUpdate);

            
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName", bookToUpdate.PublisherID);
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName", bookToUpdate.AuthorID);

            return Page();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}