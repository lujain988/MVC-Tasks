using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Code.Models
{
    public class ApplacationDbContext : DbContext
    {
           public ApplacationDbContext() : base("SchoolEntities")
    {
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Students> Students { get; set; }
    public DbSet<Asign> Asigns { get; set; }
    public DbSet<studentDetails> StudentDetails { get; set; }
    public DbSet<course> Courses { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            modelBuilder.Entity<Students>()
           .HasOptional(s => s.StudentDetails) 
           .WithRequired(sd => sd.Students);

            modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Courses)
            .WithRequired(c => c.teacher)
            .HasForeignKey(c => c.TeacherID);

        base.OnModelCreating(modelBuilder);
    }
    }
}