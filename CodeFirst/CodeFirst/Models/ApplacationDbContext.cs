using CodeFirst.Models;
using System.Data.Entity;

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
            .HasOptional(sd => sd.StudentDetails)
            .WithRequired(s => s.Students);

        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Courses)
            .WithRequired(c => c.teacher)
            .HasForeignKey(c => c.TeacherID);

        base.OnModelCreating(modelBuilder);
    }
}
