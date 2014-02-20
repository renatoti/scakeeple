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
    /// Classe responsável pelo mapeamento da entidade College para utilização do CodeFirst
    /// </summary>
    public class CollegeMapping : EntityTypeConfiguration<College>
    {
        public CollegeMapping()
        {
            ToTable("College");

            HasKey(x => x.ID);

            Property(x => x.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.Adress)
                .HasMaxLength(80);
        }
    }
}
