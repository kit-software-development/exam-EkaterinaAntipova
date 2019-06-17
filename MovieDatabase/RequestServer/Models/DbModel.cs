using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestServer.Models
{
    public class DbModel: DbContext
    {
        public DbModel()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Movies.db");
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
    }
}
