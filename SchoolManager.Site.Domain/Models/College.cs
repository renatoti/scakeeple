using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Site.Domain.Models
{
    [Serializable]
    public class College
    {
        public College()
        {
            Classrooms = new List<Classroom>();    
        }

        public int ID { get; set; }
        [Required(ErrorMessage = "O campo NOME é obrigatório")]
        public string Name { get; set; }
        public string Adress { get; set; }

        public virtual List<Classroom> Classrooms { get; set; } 
    }
}
