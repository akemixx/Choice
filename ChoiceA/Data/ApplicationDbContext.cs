using System;
using System.Collections.Generic;
using System.Text;
using ChoiceA.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChoiceA.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Discipline> Discipline { get; set; }
        public DbSet<StudDisc> StudDisc { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudDisc>()
                 .HasKey(e => new { e.StudentId, e.DisciplineId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
