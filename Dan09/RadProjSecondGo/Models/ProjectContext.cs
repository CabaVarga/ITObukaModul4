using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RadProjSecondGo.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext() : base()
        {
            // Database.SetInitializer<ProjectContext>(new DropCreateDatabaseAlways<ProjectContext>());

            Database.SetInitializer<ProjectContext>(new ProjectContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(t => new { t.EmployeeId, t.ProjectId });

            modelBuilder.Entity<EmployeeProject>()
                .HasRequired(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<EmployeeProject>()
                .HasRequired(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}