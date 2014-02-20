using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using SchoolManager.Site.Domain.Mapping;
using SchoolManager.Site.Data.Migrations;
using SchoolManager.Site.Domain.Models;

namespace SchoolManager.Site.Data
{
    public class Context : DbContext
    {
        public Context() : base("KeepleDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
        }

        public DbSet<College> Colleges { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new CollegeMapping());
            //modelBuilder.Configurations.Add(new ClassroomMapping());
            modelBuilder.Configurations.AddFromAssembly(Assembly.Load("SchoolManager.Site.Domain"));
        }
    }
}