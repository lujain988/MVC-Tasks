using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeFirst.Models
{
    public class ApplacationDbContext : DbContext
    {
        public ApplacationDbContext() : base("SchoolEntities")
        {
            
        }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Students4> students4 { get; set; }
        public DbSet<Asign> asigns { get; set; }
          protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }
}