using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Site.Domain.Models
{
    [Serializable]
    public class Classroom
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "O campo ESCOLA é obrigatório")]
        public int CollegeID { get; set; }
        public int? GraduationYear { get; set; }
        public int? Students { get; set; }
        public string Coordinator { get; set; }
        [EmailAddress(ErrorMessage = "Informe um EMAIL válido")]
        public string Email { get; set; }
        public string Period { get; set; }

        public virtual College College { get; set; }
    }
}
