using System.ComponentModel.DataAnnotations.Schema;
using SchoolManager.Site.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Site.Domain.Mapping
{
    /// <summary>
    /// Classe responsável pelo mapeamento da entidade Classroom para utilização do CodeFirst
    /// </summary>
    public class ClassroomMapping : EntityTypeConfiguration<Classroom>
    {
        public ClassroomMapping()
        {
            ToTable("Classroom");

            HasKey(x => x.ID);

            HasRequired(x => x.College)
                .WithMany(x => x.Classrooms)
                .HasForeignKey(x => x.CollegeID);

            Property(x => x.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.GraduationYear)
                .IsOptional();

            Property(x => x.Students)
                .IsOptional();

            Property(x => x.Coordinator)
                .HasMaxLength(50);

            Property(x => x.Email)
                .HasMaxLength(100);

            Property(x => x.Period)
                .HasMaxLength(1);
        }
    }
}
