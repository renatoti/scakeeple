using SchoolManager.Site.Domain.Models;

namespace SchoolManager.Site.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            //context.Colleges.AddOrUpdate(
            //  p => p.Name,
            //  new College { Name = "Faculdade 1", Adress = "Endereço da Faculdade 1" },
            //  new College { Name = "Faculdade 2", Adress = "Endereço da Faculdade 2" }
            //);

            //context.SaveChanges();

            //context.Classrooms.AddOrUpdate(
            //  p => new { p.CollegeID, p.Coordinator, p.Email, p.Period },
            //  new Classroom { CollegeID = 1, GraduationYear = 2014, Students = 30, Coordinator = "Coordenador 1", Email = "class1@google.com", Period = "5" },
            //  new Classroom { CollegeID = 1, GraduationYear = 2014, Students = 50, Coordinator = "Coordenador 2", Email = "class2@google.com", Period = "1" },
            //  new Classroom { CollegeID = 2, GraduationYear = 2015, Students = 40, Coordinator = "Coordenador 3", Email = "class3@google.com", Period = "2" }
            //);

            //context.SaveChanges();
        }
    }
}
