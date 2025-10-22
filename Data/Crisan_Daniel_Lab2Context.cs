using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Crisan_Daniel_Lab2.Models;

namespace Crisan_Daniel_Lab2.Data
{
    public class Crisan_Daniel_Lab2Context : DbContext
    {
        public Crisan_Daniel_Lab2Context (DbContextOptions<Crisan_Daniel_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Crisan_Daniel_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Crisan_Daniel_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Crisan_Daniel_Lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<Crisan_Daniel_Lab2.Models.Category> Category { get; set; } = default!;
    }
}
